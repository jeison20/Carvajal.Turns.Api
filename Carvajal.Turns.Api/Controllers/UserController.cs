using Carvajal.Turns.Utils.Gateways.Interfaces;
using Carvajal.Turns.Utils.Interfaces;
using Component;
using Data;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Carvajal.Turns.Domain;

namespace Carvajal.Turns.Api.Controllers
{
    public class UserController : ApiController
    {
        private IMessageUtils MessageUtils { get; set; }
        private IMsmqServiceGateway MailManService { get; set; }
        private IUtils Utils { get; set; }
        private IApplicationContext Ctx { get; set; }

        public UserController()
        {
            try
            {
                Ctx = ContextRegistry.GetContext();
                MailManService = (IMsmqServiceGateway)Ctx.GetObject("MailManServiceGateway");
                Utils = (IUtils)Ctx.GetObject("Utils");
                MessageUtils = (IMessageUtils)Ctx.GetObject("Template");
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: User
        [AllowAnonymous]
        [HttpGet]
        [Route("api/User/Get_ValidaLogin")]
        public async Task<HttpResponseMessage> Get_ValidaLogin()
        {
            try
            {
                var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
                string User = string.Empty;
                Users ObjectUser = new Users();

                if (CToken.Instance.ValidToken(tokenRequest, out User))
                {
                    if (!string.IsNullOrEmpty(User))
                    {
                        ObjectUser = Component.CUsers.Instance.SearchUser(User);

                        if (!ObjectUser.Status)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M27"),
                                                       new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }

                        #region Retorna informacion

                        if (ObjectUser != null)
                        {
                            ObjectUser.OldPassword = "";
                            ObjectUser.Password = "";

                            return Request.CreateResponse<Users>(HttpStatusCode.OK, ObjectUser,
                                                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M7"),
                                                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }

                        #endregion Retorna informacion
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, CToken.Instance.SearchDetailInvalidToken(tokenRequest),
                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                }

                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
            catch
            {
                #region Retorna informacion

                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

                #endregion Retorna informacion
            }
        }

        // GET: User
        [AllowAnonymous]
        [HttpGet]
        [Route("api/User/Get_MenuForRol")]
        public async Task<HttpResponseMessage> Get_MenuForRol(string Rol)
        {
            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
            string User = string.Empty;
            List<Options> ObjectOptions = new List<Options>();

            if (Component.CToken.Instance.ValidToken(tokenRequest, out User))
            {
                if (!string.IsNullOrEmpty(User))
                {
                    ObjectOptions = Component.COptionsRol.Instance.SearchOptionsForRol(Rol);

                    #region Retorna informacion

                    return Request.CreateResponse<List<Options>>(HttpStatusCode.OK, ObjectOptions,
                            new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

                    #endregion Retorna informacion
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, CToken.Instance.SearchDetailInvalidToken(tokenRequest),
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            #region Retorna informacion

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Error, Acceso denegado  ",
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

            #endregion Retorna informacion
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/User/request_new_password")]
        public async Task<HttpResponseMessage> Request_New_Password(string Email, string PkUser)
        {
            var dato = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString());
            string codeRecovery = System.Convert.ToBase64String(dato);
            codeRecovery = codeRecovery.Substring(codeRecovery.Length - 6);
            Users User = new Users();
            User = CUsers.Instance.SearchUser(PkUser);

            if (User == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M7"),
                      new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
            if (!User.Status || !User.Email.Equals(Email))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M27"),
                     new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            try
            {
                User.ChangePasswordNextTime = true;

                if (User.OldPassword.Split('|').Length == 5)
                {
                    string Password = string.Empty;
                    int Index = 0;
                    foreach (var item in User.OldPassword.Split('|'))
                    {
                        Index++;
                        if (Index != 1)
                            Password = Password + "|" + item;
                    }

                    Password = Password.Substring(1, Password.Length - 1);
                    User.OldPassword = Password;
                }

                User.OldPassword = string.IsNullOrEmpty(User.OldPassword) ? User.Password : (User.OldPassword + "|" + User.Password);
                User.LastChangeDate = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["TimeRestorePassword"]));
                User.Password = codeRecovery;
                CUsers.Instance.UpdateUser(User);

                var msg = new Carvajal.Turns.Domain.Entities.Message
                {
                    From = new Domain.Entities.Address(ConfigurationManager.AppSettings["emailFrom"]),
                    To = new Domain.Entities.Address(User.Email),
                    IsBodyHtml = true,
                    Body = MessageUtils.GetTemplateMailRecoveryPasswor(User.Name, codeRecovery),
                    Subject = ConfigurationManager.AppSettings["subjectRecoveryPassword"],
                };

                MailManService.Send(msg);
                return Request.CreateResponse(HttpStatusCode.OK, Utils.GetResourceMessages("M33"),
                                         new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                     new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/User/Set_Restore_Password")]
        public async Task<HttpResponseMessage> Set_Restore_Password(string User, string NewPassword, string ComfirmPassword, string Code)
        {
            if (NewPassword.Length != 8)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M28"),
                                                           new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];

            if (Component.CToken.Instance.ValidToken(tokenRequest))
            {
                Users ObjectUser = new Users();

                Regex rex = new Regex("^[A-Z0-9 a-z]*$");

                if (!string.IsNullOrEmpty(User))
                {
                    if (rex.IsMatch(User))
                    {
                        ObjectUser = Component.CUsers.Instance.SearchUser(User);

                        #region Retorna informacion

                        if (ObjectUser != null)
                        {
                            if (!ObjectUser.Status)
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M27"),
                                                                                       new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }

                            if (!ObjectUser.Password.Equals(Code))
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M27"),
                                                                                       new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }

                            if (!NewPassword.Equals(ComfirmPassword))
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M29"),
                                                                                  new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }

                            ObjectUser.Password = NewPassword;
                            if (ObjectUser.OldPassword.Split('|').Length == 5)
                            {
                                string Password = string.Empty;
                                int Index = 0;
                                foreach (var item in ObjectUser.OldPassword.Split('|'))
                                {
                                    Index++;
                                    if (Index != 1)
                                        Password = Password + "|" + item;
                                }

                                Password = Password.Substring(1, Password.Length - 1);
                                ObjectUser.OldPassword = Password;
                            }
                            ObjectUser.OldPassword = ObjectUser.OldPassword + "|" + NewPassword;
                            ObjectUser.ChangePasswordNextTime = false;
                            ObjectUser.LastChangeDate = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["TimeRestorePassword"]));

                            if (CUsers.Instance.UpdateUser(ObjectUser))
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Utils.GetResourceMessages("M30"),
                                                                                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                                                                                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M7"),
                                                               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }

                        #endregion Retorna informacion
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M7"),
                                                           new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                    }
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, CToken.Instance.SearchDetailInvalidToken(tokenRequest),
                                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Usuario sin acceso",
                         new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/User/set_Change_password")]
        public async Task<HttpResponseMessage> SetChangePassword(string User, string NewPassword, string ComfirmPassword, string OldPassword)
        {
            if (NewPassword.Length != 8)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M28"),
                                                           new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];

            if (Component.CToken.Instance.ValidToken(tokenRequest))
            {
                Users ObjectUser = new Users();

                Regex rex = new Regex("^[A-Z0-9 a-z]*$");

                if (!string.IsNullOrEmpty(User))
                {
                    if (rex.IsMatch(User))
                    {
                        ObjectUser = Component.CUsers.Instance.SearchUser(User);

                        #region Retorna informacion

                        if (ObjectUser != null)
                        {
                            if (!ObjectUser.Status)
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M27"),
                                                                                       new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }

                            if (!OldPassword.Equals(ObjectUser.Password))
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M7"),
                                                                                  new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }

                            if (!NewPassword.Equals(ComfirmPassword))
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M29"),
                                                                                  new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }

                            if (NewPassword.Equals(ObjectUser.Password) || ObjectUser.OldPassword.Contains(NewPassword))
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M34"),
                                                                                  new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }

                            ObjectUser.Password = NewPassword;
                            ObjectUser.ChangePasswordNextTime = false;

                            if (ObjectUser.OldPassword.Split('|').Length == 5)
                            {
                                string Password = string.Empty;
                                int Index = 0;
                                foreach (var item in ObjectUser.OldPassword.Split('|'))
                                {
                                    Index++;
                                    if (Index != 1)
                                        Password = Password + "|" + item;
                                }

                                Password = Password.Substring(1, Password.Length - 1);
                                ObjectUser.OldPassword = Password;
                            }

                            ObjectUser.OldPassword = ObjectUser.OldPassword + "|" + NewPassword;

                            ObjectUser.LastChangeDate = DateTime.Now.AddDays(Convert.ToInt32(ConfigurationManager.AppSettings["TimeRestorePassword"]));

                            if (CUsers.Instance.UpdateUser(ObjectUser))
                            {
                                return Request.CreateResponse(HttpStatusCode.OK, Utils.GetResourceMessages("M30"),
                                                                                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                                                                                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M7"),
                                                               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }

                        #endregion Retorna informacion
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Usuario Invalido",
                                                           new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                    }
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, CToken.Instance.SearchDetailInvalidToken(tokenRequest),
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Usuario sin acceso",
                         new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/User/Get_Info_User")]
        public async Task<HttpResponseMessage> Get_Info_User()
        {
            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
            string User = string.Empty;
            Users ObjectUser = new Users();

            Domain.Entities.Users ObjectUserDomain = new Domain.Entities.Users();
            if (CToken.Instance.ValidToken(tokenRequest))
            {
                User = CToken.Instance.GetUserToken(tokenRequest);

                if (string.IsNullOrEmpty(User))
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                }

                ObjectUser = CUsers.Instance.SearchUser(User);
                if (ObjectUser == null)
                {
                    Companies ObjectCompany = CCompanies.Instance.SearchCompany(ObjectUser.FkCompanies_Identifier);

                    if (ObjectCompany == null)
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                    }

                    ObjectUserDomain.FkCompanies_Identifier = ObjectCompany.Name;
                    ObjectUserDomain.Name = ObjectUser.Name;
                    ObjectUserDomain.PkIdentifier = ObjectUser.PkIdentifier;
                    ObjectUserDomain.Email = ObjectUser.Email;
                    ObjectUserDomain.Phone = ObjectUser.Phone;
                    ObjectUserDomain.Address = ObjectUser.Address;
                    if (!ObjectUser.FkRole_Identifier.Equals(ConfigurationManager.AppSettings["RolAdmin"]))
                    {
                        Centres ObjectCenter = CCentres.Instance.SearchCenterResponsibleUser(ObjectUser.PkIdentifier);
                        ObjectUserDomain.Center = ObjectCenter == null ? null : ObjectCenter.Name;
                    }

                    #region Retorna informacion

                    return Request.CreateResponse<Carvajal.Turns.Domain.Entities.Users>(HttpStatusCode.OK, ObjectUserDomain,
                            new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

                    #endregion Retorna informacion
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                                new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, CToken.Instance.SearchDetailInvalidToken(tokenRequest),
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/User/GetTypeUserRol")]
        public async Task<HttpResponseMessage> GetTypeUserRol(string Rol)
        {
            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
            string User = string.Empty;
            List<Roles> ListObjectRol = new List<Roles>();

            if (CToken.Instance.ValidToken(tokenRequest))
            {
                #region Retorna informacion

                ListObjectRol = CRoles.Instance.SearchTypeUserRols(Rol);

                return Request.CreateResponse(HttpStatusCode.OK, ListObjectRol,
                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

                #endregion Retorna informacion
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, CToken.Instance.SearchDetailInvalidToken(tokenRequest),
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/User/GetContactosRol")]
        public async Task<HttpResponseMessage> GetContactosRol(string Rol)
        {
            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
            string User = string.Empty;
            Users ObjectUser = new Users();
            List<Users> ListObjectUser = new List<Users>();

            if (CToken.Instance.ValidToken(tokenRequest))
            {
                #region Retorna informacion

                User = CToken.Instance.GetUserToken(tokenRequest);
                ObjectUser = CUsers.Instance.SearchUser(User);

                ListObjectUser = CUsers.Instance.SearchUserRolCompany(ObjectUser, Rol);

                if (ListObjectUser != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ListObjectUser,
                                           new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                         new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                }

                #endregion Retorna informacion
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, CToken.Instance.SearchDetailInvalidToken(tokenRequest),
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }
    }
}