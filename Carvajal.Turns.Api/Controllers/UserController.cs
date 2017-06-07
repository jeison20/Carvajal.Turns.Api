using Carvajal.Turns.Utils.Gateways.Interfaces;
using Carvajal.Turns.Utils.Interfaces;
using Component;
using Data;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Carvajal.Turns.Api.Controllers
{
    [RoutePrefix("api/User")]
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

        /// <summary>
        /// Metodo encargado de validar que el token suministrado este activo y sea valido
        /// </summary>
        /// <returns>un objeto con informacion relacionada con el usuario autenticado</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("ValidaLogin/Get")]
        public async Task<HttpResponseMessage> ValidaLogin()
        {
            try
            {
                var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
                string User = string.Empty;
                Users ObjectUser = new Users();
                Domain.Entities.Users ObjectUserDomain = new Domain.Entities.Users();
                if (CClient.Instance.ValidToken(tokenRequest, out User))
                {
                    if (!string.IsNullOrEmpty(User))
                    {
                        ObjectUser = CUsers.Instance.SearchUser(User);

                        if (!ObjectUser.Status)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M27"),
                                                       new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }

                        #region Retorna informacion

                        if (ObjectUser != null)
                        {
                            string Center = string.Empty;
                            List<Centres> ObjectListCentres = new List<Centres>();
                            if (!ObjectUser.FkRole_Identifier.Equals("FA"))
                            {
                                ObjectListCentres = CLinkedCentres.Instance.SearchCentresForUser(ObjectUser.PkIdentifier);

                                if (ObjectListCentres != null)
                                {
                                    foreach (var item in ObjectListCentres)
                                    {
                                        if (string.IsNullOrEmpty(Center))
                                            Center = item.Name;
                                        else
                                            Center = Center + "," + item.Name;
                                    }
                                }
                            }
                            else
                            {
                                Companies Compani = CCompanies.Instance.SearchCompany(ObjectUser.FkCompanies_Identifier);
                                Center = Compani.Name;
                            }

                            Roles ObjectRol = CRoles.Instance.SearchRol(ObjectUser.FkRole_Identifier);
                            ObjectUserDomain.Address = ObjectUser.Address;
                            ObjectUserDomain.Center = Center;
                            ObjectUserDomain.Email = ObjectUser.Email;
                            ObjectUserDomain.FkCountries_Identifier = ObjectUser.FkCountries_Identifier.ToString();
                            ObjectUserDomain.FkRole_Identifier = ObjectUser.FkRole_Identifier;
                            ObjectUserDomain.FkRole_Name = ObjectRol.Name;
                            ObjectUserDomain.Name = ObjectUser.Name;
                            ObjectUserDomain.PkIdentifier = ObjectUser.PkIdentifier;
                            ObjectUserDomain.Status = ObjectUser.Status;
                            ObjectUserDomain.UrlInitialRol = ObjectRol.InitialUrl;

                            return Request.CreateResponse(HttpStatusCode.OK, ObjectUserDomain,
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
                    return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
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

        /// <summary>
        /// Metodo encargado de retornar el menu correspondiente al rol solicitado
        /// </summary>
        /// <param name="Rol">rol al cual esta asociado un usuario</param>
        /// <returns>Objeto de tipo Options con las diferentes opciones del menu </returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("MenuForRol/Get")]
        public async Task<HttpResponseMessage> MenuForRol(string Rol)
        {
            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
            string User = string.Empty;
            List<Options> ObjectOptions = new List<Options>();

            if (CClient.Instance.ValidToken(tokenRequest, out User))
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            #region Retorna informacion

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Error, Acceso denegado  ",
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));

            #endregion Retorna informacion
        }

        /// <summary>
        /// Metodo encargado de realizar la solicitud de un nuevo password
        /// </summary>
        /// <param name="Email">Email del usuario el cual se le restaurara la clave </param>
        /// <param name="PkUser"></param>
        /// <returns>retorna un mensaje donde especifica el resultado de la operacion</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("RequestNewPassword/Get")]
        public async Task<HttpResponseMessage> RequestNewPassword(string Email, string PkUser)
        {
            var dato = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
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
                if (CUsers.Instance.UpdateUser(User))
                {
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
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                }
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("ErrorConsultBD", "0", "SearchUserCompany" + "BGM" + ex.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                     new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }

        /// <summary>
        /// Metodo encargado de restaurar la contraseña tras realizar el proceso de solicitud de restablecimiento
        /// </summary>
        /// <param name="Event">Objeto que provee de los parametros para realizar el restablecimiento de la contraseña  </param>
        /// <returns>Mensaje con el resultado de la operacion</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("RestorePassword/Post")]
        public async Task<HttpResponseMessage> RestorePassword([FromBody]Domain.Request.RequestCreateEvent Event)
        {
            string User = Event.User;
            string NewPassword = Event.NewPassword;
            string ComfirmPassword = Event.ComfirmPassword;
            string Code = Event.Code;

            if (NewPassword.Length != 8)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M28"),
                                                           new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];

            if (CClient.Instance.ValidToken(tokenRequest))
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
                                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Usuario sin acceso",
                         new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }

        /// <summary>
        /// Metodo encargado de realizar el cambio de contraseña
        /// </summary>
        /// <param name="Event">Objeto que provee de los parametros para realizar el cambio de la contraseña</param>
        /// <returns>Mensaje con el resultado de la operacion</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("ChangePassword/Post")]
        public async Task<HttpResponseMessage> ChangePassword([FromBody]Domain.Request.RequestCreateEvent Event)
        {
            string User = Event.User;
            string NewPassword = Event.NewPassword;
            string ComfirmPassword = Event.ComfirmPassword;
            string OldPassword = Event.Code;

            if (NewPassword.Length != 8)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M28"),
                                                           new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];

            if (CClient.Instance.ValidToken(tokenRequest))
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
                    new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }

            return Request.CreateResponse(HttpStatusCode.BadRequest, "Usuario sin acceso",
                         new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
        }

        /// <summary>
        /// Metodo que consulta la informacion del usuario logueado
        /// </summary>
        /// <returns>Objecto con la informacion pertinente al usuario</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("InfoUser/Get")]
        public async Task<HttpResponseMessage> InfoUser()
        {
            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
            string User = string.Empty;
            Users ObjectUser = new Users();

            Domain.Entities.Users ObjectUserDomain = new Domain.Entities.Users();
            if (CClient.Instance.ValidToken(tokenRequest))
            {
                User = CClient.Instance.GetUserToken(tokenRequest);

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
                        List<Centres> Centres = CLinkedCentres.Instance.SearchCentresForUser(ObjectUser.PkIdentifier);
                        if (Centres != null && Centres.Count > 0)
                            ObjectUserDomain.Center = Centres[0].Name;
                    }
                    else
                    {
                        List<Centres> ObjectListCentres = new List<Centres>();
                        ObjectListCentres = CLinkedCentres.Instance.SearchCentresForUser(ObjectUser.PkIdentifier);
                        if (ObjectListCentres != null)
                        {
                            string Center = string.Empty;
                            foreach (var item in ObjectListCentres)
                            {
                                if (string.IsNullOrEmpty(Center))
                                    Center = item.Name;
                                else
                                    Center = Center + "," + item.Name;
                            }
                        }
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }

        /// <summary>
        /// Metodo que consulta los tipos de usuario sobre los cuales el rol suministrado tiene permisos
        /// </summary>
        /// <param name="Role">Rol a consultar </param>
        /// <returns>Object con una lista de roles </returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("TypeUserRol/Get")]
        public async Task<HttpResponseMessage> TypeUserRole(string Role)
        {
            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
            string User = string.Empty;
            List<Roles> ListObjectRol = new List<Roles>();

            if (CClient.Instance.ValidToken(tokenRequest))
            {
                #region Retorna informacion

                ListObjectRol = CRoles.Instance.SearchTypeUserRols(Role);
                if (ListObjectRol != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, ListObjectRol,
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }

        /// <summary>
        /// Metodo que consulta los centros asociados al usuario autenticado
        /// </summary>
        /// <returns>Objeto con una lista de LinkedCentres </returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("CentresForUser/Get")]
        public async Task<HttpResponseMessage> CentresForUser()
        {
            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
            string User = string.Empty;
            User = CClient.Instance.GetUserToken(tokenRequest);
            List<Centres> ListObjectCentres = new List<Centres>();

            if (CClient.Instance.ValidToken(tokenRequest))
            {
                #region Retorna informacion

                ListObjectCentres = CLinkedCentres.Instance.SearchCentresForUser(User);
                if (ListObjectCentres != null)
                {
                    var Data = (from o in ListObjectCentres select new { Identification = o.PkIdentifier, Name = o.Name }).ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, Data,
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }

        /// <summary>
        /// Metodo que consulta los usuarios asociados a los centros a los que pertenece el usuario autenticado
        /// </summary>
        /// <param name="Rol">rol a consultar</param>
        /// <returns>Objeto lista de usuarios asociados a los centros y con el tipo de rol suministrado </returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("ContactsRole/Get")]
        public async Task<HttpResponseMessage> ContactsRole(string Rol)
        {
            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
            string User = string.Empty;
            Users ObjectUser = new Users();
            List<Users> ListObjectUser = new List<Users>();

            if (CClient.Instance.ValidToken(tokenRequest))
            {
                #region Retorna informacion

                User = CClient.Instance.GetUserToken(tokenRequest);
                if (!string.IsNullOrEmpty(User))
                {
                    ObjectUser = CUsers.Instance.SearchUser(User);

                    ListObjectUser = CUsers.Instance.SearchUserRolCompany(ObjectUser, Rol);

                    if (ListObjectUser != null)
                    {
                        if (ListObjectUser.Count > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, ListObjectUser,
                                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M10"),
                                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                             new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                    }
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }

        /// <summary>
        /// Metodo que consulta usuarios
        /// </summary>
        /// <param name="Rol">Rol al cual deben pertenecer los usuarios de la consulta </param>
        /// <param name="Identification">numero de identificacion del usuario a buscar</param>
        /// <param name="Name">nombre del usuario a consultar</param>
        /// <param name="Active">estado del usuario que se desea buscar</param>
        /// <param name="DeliveryCenter">centro al que pertenece el usuario a buscar</param>
        /// <returns>Objecto con una lista de usuarios que cumplen con los filtros manejados</returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("ManagerUsers/Get")]
        public async Task<HttpResponseMessage> ManagerUsers(string Rol, string Identification, string Name, string Active, string DeliveryCenter)
        {
            if (string.IsNullOrEmpty(Rol) && string.IsNullOrEmpty(Identification))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format(Utils.GetResourceMessages("M5"), "Tipo de usuario"),
                                          new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
            var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
            string User = string.Empty;
            Users ObjectUser = new Users();
            List<Carvajal.Turns.Domain.Entities.Users> ListObjectUser = new List<Carvajal.Turns.Domain.Entities.Users>();

            if (CClient.Instance.ValidToken(tokenRequest))
            {
                #region Retorna informacion

                User = CClient.Instance.GetUserToken(tokenRequest);
                ObjectUser = CUsers.Instance.SearchUser(User);
                if (!string.IsNullOrEmpty(User))
                {
                    if (string.IsNullOrEmpty(Active))
                        ListObjectUser = CUsers.Instance.SearchUserCompany(ObjectUser, Rol, Identification, Name, null, DeliveryCenter);
                    else
                        ListObjectUser = CUsers.Instance.SearchUserCompany(ObjectUser, Rol, Identification, Name, Active == "1" ? true : false, DeliveryCenter);

                    if (ListObjectUser != null)
                    {
                        if (ListObjectUser.Count > 0)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, ListObjectUser,
                                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M10"),
                                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                             new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                    }
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
                return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }

        /// <summary>
        /// Metodo que permite actualizar un usuario
        /// </summary>
        /// <param name="Event">Objeto que contiene toda la informacion que se puede modificar del usuario</param>
        /// <returns>mensaje con el resultado de la operacion</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("ModifyUsers/Post")]
        public async Task<HttpResponseMessage> ModifyUsers([FromBody]Domain.Request.RequestCreateEventUser Event)
        {
            try
            {
                var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
                string User = string.Empty;
                Users AuthenticatedUser = new Users();
                if (CClient.Instance.ValidToken(tokenRequest))
                {
                    User = CClient.Instance.GetUserToken(tokenRequest);
                    bool ValidSendEmail = false;
                    AuthenticatedUser = CUsers.Instance.SearchUser(User);
                    if (AuthenticatedUser.FkRole_Identifier.Equals(ConfigurationManager.AppSettings["UserAdmin"]))
                    {
                        if (string.IsNullOrEmpty(Event.PkIdentifier) || string.IsNullOrEmpty(Event.Name) || string.IsNullOrEmpty(Event.FkRole_Identifier) || string.IsNullOrEmpty(Event.Email))
                        {
                            string Field = string.Empty;

                            if (string.IsNullOrEmpty(Event.PkIdentifier))
                                Field = "Identifier";

                            if (string.IsNullOrEmpty(Event.Name))
                            {
                                if (string.IsNullOrEmpty(Field))
                                    Field = "Name";
                                else
                                    Field = Field + "," + "Name";
                            }

                            if (string.IsNullOrEmpty(Event.FkRole_Identifier))
                            {
                                if (string.IsNullOrEmpty(Field))
                                    Field = "Type of user";
                                else
                                    Field = Field + "," + "Type of user";
                            }
                            if (string.IsNullOrEmpty(Event.Email))
                            {
                                if (string.IsNullOrEmpty(Field))
                                    Field = "Email";
                                else
                                    Field = Field + "," + "Email";
                            }

                            return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format(Utils.GetResourceMessages("M5"), Field),
                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }

                        Users ObjectUser = new Users();
                        ObjectUser = CUsers.Instance.SearchUser(Event.PkIdentifier);

                        if (ObjectUser != null)
                        {
                            if (!AuthenticatedUser.FkCompanies_Identifier.Equals(ObjectUser.FkCompanies_Identifier))
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M99"),
                                                                       new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }
                            if (ObjectUser.Email.Trim().ToUpper() != Event.Email.Trim().ToUpper())
                            {
                                ValidSendEmail = true;
                                Users UserEmail = CUsers.Instance.SearchUserEmail(Event.Email);
                                if (UserEmail != null)
                                {
                                    if (!string.IsNullOrEmpty(UserEmail.PkIdentifier))
                                    {
                                        return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M38"),
                                                                     new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                                    }
                                }
                            }

                            if (CLinkedCentres.Instance.UpdateCenterUser(Event.PkIdentifier, Event.Centres, Event.FkRole_Identifier))
                            {
                                ObjectUser.Name = Event.Name;
                                ObjectUser.FkRole_Identifier = Event.FkRole_Identifier;
                                ObjectUser.Email = Event.Email;
                                ObjectUser.Status = Event.Status;
                                CUsers.Instance.SaveChanges();

                                if (ValidSendEmail)
                                {
                                    var msg = new Carvajal.Turns.Domain.Entities.Message
                                    {
                                        From = new Domain.Entities.Address(ConfigurationManager.AppSettings["emailFrom"]),
                                        To = new Domain.Entities.Address(ObjectUser.Email),
                                        IsBodyHtml = true,
                                        Body = MessageUtils.GetTemplateChangeMail(ObjectUser.Name, ObjectUser.Password),
                                        Subject = ConfigurationManager.AppSettings["subjectChangeEmail"],
                                    };

                                    MailManService.Send(msg);
                                }

                                return Request.CreateResponse(HttpStatusCode.OK, Utils.GetResourceMessages("M36"),
                                  new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M65"),
                                  new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }
                        }
                        else
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "User without permissions",
                               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }

        /// <summary>
        /// Metodo que permite crear un usuario
        /// </summary>
        /// <param name="Event">Objeto con la lista de parametros necesarios para la creacion del usuario</param>
        /// <returns>Mensaje con el estado de la operacion</returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateUsers/Post")]
        public async Task<HttpResponseMessage> CreateUsers([FromBody]Domain.Request.RequestCreateEventUser Event)
        {
            try
            {
                string Password = string.Empty;
                Password = CUsers.Instance.GetRandomText(8);

                var tokenRequest = HttpContext.Current.GetOwinContext().Request.Headers.Get("Authorization").Split(' ')[1];
                string User = string.Empty;
                Users AuthenticatedUser = new Users();
                if (CClient.Instance.ValidToken(tokenRequest))
                {
                    User = CClient.Instance.GetUserToken(tokenRequest);

                    AuthenticatedUser = CUsers.Instance.SearchUser(User);
                    if (AuthenticatedUser.FkRole_Identifier.Equals(ConfigurationManager.AppSettings["UserAdmin"]))
                    {
                        if (string.IsNullOrEmpty(Event.PkIdentifier) || string.IsNullOrEmpty(Event.Name) || string.IsNullOrEmpty(Event.FkRole_Identifier) || string.IsNullOrEmpty(Event.Email))
                        {
                            string Field = string.Empty;

                            if (string.IsNullOrEmpty(Event.PkIdentifier))
                                Field = "Identifier";

                            if (string.IsNullOrEmpty(Event.Name))
                            {
                                if (string.IsNullOrEmpty(Field))
                                    Field = "Name";
                                else
                                    Field = Field + "," + "Name";
                            }

                            if (string.IsNullOrEmpty(Event.FkRole_Identifier))
                            {
                                if (string.IsNullOrEmpty(Field))
                                    Field = "Type of user";
                                else
                                    Field = Field + "," + "Type of user";
                            }
                            if (string.IsNullOrEmpty(Event.Email))
                            {
                                if (string.IsNullOrEmpty(Field))
                                    Field = "Email";
                                else
                                    Field = Field + "," + "Email";
                            }

                            return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format(Utils.GetResourceMessages("M5"), Field),
                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }

                        Users ObjectUser = new Users();
                        ObjectUser = CUsers.Instance.SearchUser(Event.PkIdentifier);

                        if (ObjectUser != null && ObjectUser.Status == true)
                        {
                            return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M40"),
                                                                       new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                        }

                        if (ObjectUser != null)
                        {
                            if (ObjectUser.Email.Trim().ToUpper() != Event.Email.Trim().ToUpper())
                            {
                                if (CUsers.Instance.ValidUniqueUserEmail(Event.Email, Event.PkIdentifier))
                                {
                                    return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M38"),
                                                                 new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                                }
                            }

                            if (CLinkedCentres.Instance.UpdateCenterUser(Event.PkIdentifier, Event.Centres, Event.FkRole_Identifier))
                            {
                                ObjectUser.Password = Password;
                                ObjectUser.Name = Event.Name;
                                ObjectUser.FkRole_Identifier = Event.FkRole_Identifier;
                                ObjectUser.Email = Event.Email;
                                ObjectUser.Status = Event.Status;
                                ObjectUser.FkCompanies_Identifier = AuthenticatedUser.FkCompanies_Identifier;

                                CUsers.Instance.SaveChanges();

                                var msg = new Domain.Entities.Message
                                {
                                    From = new Domain.Entities.Address(ConfigurationManager.AppSettings["emailFrom"]),
                                    To = new Domain.Entities.Address(ObjectUser.Email),
                                    IsBodyHtml = true,
                                    Body = MessageUtils.GetTemplateChangeMail(ObjectUser.Name, ObjectUser.Password),
                                    Subject = ConfigurationManager.AppSettings["subjectRecoveryPassword"],
                                };

                                MailManService.Send(msg);

                                return Request.CreateResponse(HttpStatusCode.OK, Utils.GetResourceMessages("M36"),
                                  new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }
                            else
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M65"),
                                  new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }
                        }
                        else
                        {
                            if (CUsers.Instance.ValidUniqueUserEmail(Event.Email, Event.PkIdentifier))
                            {
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M38"),
                                                             new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }

                            if (CLinkedCentres.Instance.UpdateCenterUser(Event.PkIdentifier, Event.Centres, Event.FkRole_Identifier))
                            {
                                if (CUsers.Instance.SaveUser(new Users { PkIdentifier = Event.PkIdentifier, ChangePasswordNextTime = true, Password = Password, Name = Event.Name, LastAccess = null, FkRole_Identifier = Event.FkRole_Identifier, Email = Event.Email, Phone = Event.Phone, LastChangeDate = DateTime.Now, FkCompanies_Identifier = AuthenticatedUser.FkCompanies_Identifier, FkCountries_Identifier = AuthenticatedUser.FkCountries_Identifier }))
                                {
                                    var msg = new Domain.Entities.Message
                                    {
                                        From = new Domain.Entities.Address(ConfigurationManager.AppSettings["emailFrom"]),
                                        To = new Domain.Entities.Address(ObjectUser.Email),
                                        IsBodyHtml = true,
                                        Body = MessageUtils.GetTemplateChangeMail(ObjectUser.Name, ObjectUser.Password),
                                        Subject = ConfigurationManager.AppSettings["subjectRecoveryPassword"],
                                    };

                                    MailManService.Send(msg);

                                    return Request.CreateResponse(HttpStatusCode.OK, Utils.GetResourceMessages("M39"),
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
                                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                   new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                            }
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "User without permissions",
                               new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, CClient.Instance.SearchDetailInvalidToken(tokenRequest),
                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
                }
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, Utils.GetResourceMessages("M12"),
                                        new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"));
            }
        }
    }
}