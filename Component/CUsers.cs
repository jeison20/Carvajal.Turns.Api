using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Component
{
    public class CUsers : ModelData
    {
        private static CUsers _Instance = new CUsers();

        public CUsers()
      : base()
        {
        }

        public static CUsers Instance
        {
            get
            {
                return _Instance;
            }
        }

        public bool SaveUser(Users User)
        {
            try
            {
                Users.Add(User);
                Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                if (User != null)
                    LogComponent.WriteError(User.FkCompanies_Identifier, "0", "SaveUser" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "SaveUser" + "BGM" + ex.Message);
                return false;
            }
        }

        public Users SearchUser(string IdentificationNumber)
        {
            Users ObjectUser = new Users();
            try
            {
                ObjectUser = Instance.Users.FirstOrDefault(c => c.PkIdentifier == IdentificationNumber);
                return ObjectUser;
            }
            catch (Exception ex)
            {
                if (ObjectUser != null)
                    LogComponent.WriteError(ObjectUser.FkCompanies_Identifier, "0", "SearchUser" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "SearchUser" + "BGM" + ex.Message);

                return null;
            }
        }

        public bool ValidActiveUser(string IdentificationNumber, string Company)
        {
            List<Users> ObjectUser = new List<Users>();
            try
            {
                ObjectUser = Instance.Users.Where(c => c.PkIdentifier == IdentificationNumber).ToList();
                foreach (var item in ObjectUser)
                {
                }

                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("ErrorConsultBD", "0", "SearchUserMultiCenter" + "BGM" + ex.Message);

                return false;
            }
        }

        public Users SearchUser(string User, string Password)
        {
            Users ObjectUser = new Users();
            try
            {
                ObjectUser = Instance.Users.FirstOrDefault(c => c.Password == (Password) && c.PkIdentifier == (User));
                return ObjectUser;
            }
            catch (Exception ex)
            {
                if (ObjectUser != null)
                    LogComponent.WriteError(ObjectUser.FkCompanies_Identifier, "0", "SearchUser" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "SearchUser" + "BGM" + ex.Message);
                return null;
            }
        }

        public Users SearchUserEmail(string Email)
        {
            Users ObjectUser = new Users();
            try
            {
                ObjectUser = Instance.Users.FirstOrDefault(c => c.Email == (Email) && c.Status == (true));
                return ObjectUser;
            }
            catch (Exception ex)
            {
                if (ObjectUser != null)
                    LogComponent.WriteError(ObjectUser.FkCompanies_Identifier, "0", "SearchUserEmail" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "SearchUserEmail" + "BGM" + ex.Message);

                return null;
            }
        }

        public bool ValidUniqueUserEmail(string Email, string Identification)
        {
            Users ObjectUser = new Users();
            try
            {
                ObjectUser = Instance.Users.FirstOrDefault(c => c.Email == (Email) && c.PkIdentifier != Identification);
                if (ObjectUser != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("ErrorConsultBD", "0", "ValidUniqueUserEmail" + "BGM" + ex.Message);

                return false;
            }
        }

        public string GetRandomText(int LengthText)
        {
            var dato = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
            string Text = Convert.ToBase64String(dato);
            if (Text.Length > LengthText)
                Text = Text.Substring(Text.Length - LengthText);
            return Text;
        }

        public bool DeleteUser(Users User)
        {
            try
            {
                if (Users.FirstOrDefault(c => c.PkIdentifier.Equals(User.PkIdentifier)) != null)
                {
                    Users.Remove(User);
                    Instance.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                if (User != null)
                    LogComponent.WriteError(User.FkCompanies_Identifier, "0", "DeleteUser" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "DeleteUser" + "BGM" + ex.Message);
                return false;
            }
        }

        public bool UpdateUser(Users User)
        {
            Users UserUpdate = new Users();
            try
            {
                UserUpdate = Users.FirstOrDefault(c => c.PkIdentifier.Equals(User.PkIdentifier));
                if (UserUpdate != null)
                {
                    UserUpdate.ChangePasswordNextTime = User.ChangePasswordNextTime;
                    UserUpdate.Password = User.Password;
                    UserUpdate.Name = User.Name;
                    UserUpdate.LastAccess = User.LastAccess;
                    UserUpdate.FkRole_Identifier = User.FkRole_Identifier;
                    UserUpdate.Email = User.Email;
                    UserUpdate.Status = User.Status;
                    UserUpdate.Phone = User.Phone;
                    UserUpdate.LastChangeDate = User.LastChangeDate;
                    UserUpdate.FkCompanies_Identifier = User.FkCompanies_Identifier;
                    UserUpdate.FkCountries_Identifier = User.FkCountries_Identifier;
                    UserUpdate.OldPassword = User.OldPassword;
                    Instance.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                if (UserUpdate != null)
                    LogComponent.WriteError(UserUpdate.FkCompanies_Identifier, "0", "UpdateUser" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "UpdateUser" + "BGM" + ex.Message);

                return false;
            }
        }

        public string SearchMerchantUser(string User)
        {
            Users ObjectUser = new Users();
            try
            {
                ObjectUser = Instance.Users.FirstOrDefault(c => c.PkIdentifier == (User));
                if (ObjectUser != null)
                    return ObjectUser.FkCompanies_Identifier;
                else
                    return "";
            }
            catch (Exception ex)
            {
                if (ObjectUser != null)
                    LogComponent.WriteError(ObjectUser.FkCompanies_Identifier, "0", "SearchMerchantUser" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "SearchMerchantUser" + "BGM" + ex.Message);

                return "";
            }
        }

        public List<Users> SearchUserRolCompany(Users User, string PKRol, string FilterNombre = "")
        {
            List<Users> ObjectUser = new List<Users>();
            try
            {
                if (User.FkRole_Identifier.Equals(System.Configuration.ConfigurationManager.AppSettings["UserProvider"]))
                {
                    ObjectUser = Instance.Users.Where(c => c.FkRole_Identifier == PKRol && c.FkCompanies_Identifier == User.FkCompanies_Identifier).ToList();
                }
                else
                {
                    List<Centres> Centres = CLinkedCentres.Instance.SearchCentresForUser(User.PkIdentifier);
                    List<Users> Users = new List<Users>();
                    if (Centres != null)
                    {
                        foreach (var item in Centres)
                        {
                            List<Users> ListUserCenter = new List<Users>();
                            ListUserCenter = CLinkedCentres.Instance.SearchUsersForCenter(item.PkIdentifier);
                            if (Users.Count == 0)
                                Users = ListUserCenter;
                            else if (Users.Count > 0 && ListUserCenter != null && ListUserCenter.Count > 0)
                            {
                                Users.AddRange(ListUserCenter);
                            }
                        }

                        if (Users.Count > 0)
                            ObjectUser = Users;

                        ObjectUser = ObjectUser.Where(c => c.FkRole_Identifier.Equals(PKRol)).ToList();
                    }
                }

                if (!string.IsNullOrEmpty(FilterNombre) && ObjectUser.Count > 0)
                {
                    ObjectUser = ObjectUser.Where(c => c.Name.Trim().ToUpper().Contains(FilterNombre.Trim().ToUpper())).ToList();
                }
                if (ObjectUser != null)
                {
                    foreach (var item in ObjectUser)
                    {
                        item.Password = "";
                        item.OldPassword = "";
                    }

                    return ObjectUser;
                }
                else
                    return new List<Users>();
            }
            catch (Exception ex)
            {
                if (ObjectUser != null)
                    LogComponent.WriteError(User.FkCompanies_Identifier, "0", "SearchUserRolCompany" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "SearchUserRolCompany" + "BGM" + ex.Message);

                return null;
            }
        }

        public List<Carvajal.Turns.Domain.Entities.Users> SearchUserCompany(Users User, string PKRol, string FilterIdentification, string FilterName, bool? Active, string Center)
        {
            List<Users> ObjectUser = new List<Users>();
            try
            {
                List<Centres> Centres = CLinkedCentres.Instance.SearchCentresForUser(User.PkIdentifier);
                List<Users> Users = new List<Users>();
                if (Centres != null)
                {
                    if (!string.IsNullOrEmpty(Center))
                    {
                        List<Users> ListUserCenter = new List<Users>();
                        ListUserCenter = CLinkedCentres.Instance.SearchUsersForCenter(Center);
                        Users = ListUserCenter;
                    }
                    else
                    {
                        foreach (var item in Centres)
                        {
                            List<Users> ListUserCenter = new List<Users>();
                            ListUserCenter = CLinkedCentres.Instance.SearchUsersForCenter(item.PkIdentifier);
                            if (Users.Count == 0)
                                Users = ListUserCenter;
                            else if (Users.Count > 0 && ListUserCenter != null && ListUserCenter.Count > 0)
                            {
                                Users.AddRange(ListUserCenter);
                            }
                        }
                    }

                    if (Users.Count > 0)
                        ObjectUser = Users;

                    if (!string.IsNullOrEmpty(PKRol) && ObjectUser != null && ObjectUser.Count > 0)
                    {
                        ObjectUser = ObjectUser.Where(c => c.FkRole_Identifier.Equals(PKRol)).ToList();
                    }
                    if (!string.IsNullOrEmpty(FilterName) && ObjectUser != null && ObjectUser.Count > 0)
                    {
                        ObjectUser = ObjectUser.Where(c => c.Name.Trim().ToUpper().Contains(FilterName.Trim().ToUpper())).ToList();
                    }
                    if (!string.IsNullOrEmpty(FilterIdentification) && ObjectUser != null && ObjectUser.Count > 0)
                    {
                        ObjectUser = ObjectUser.Where(c => c.PkIdentifier.Trim().Equals(FilterIdentification.Trim())).ToList();
                    }
                    if (Active != null && ObjectUser != null && ObjectUser.Count > 0)
                    {
                        ObjectUser = ObjectUser.Where(c => c.Status == Active).ToList();
                    }
                }
                else if (Centres == null)
                    return null;

                if (ObjectUser != null)
                {
                    string ListCentres = string.Empty;

                    List<Carvajal.Turns.Domain.Entities.Users> ListUser = (from a in ObjectUser
                                                                           join r in Roles on a.FkRole_Identifier equals r.PkIdentifier
                                                                           select new Carvajal.Turns.Domain.Entities.Users
                                                                           {
                                                                               PkIdentifier = a.PkIdentifier,
                                                                               ChangePasswordNextTime = a.ChangePasswordNextTime,
                                                                               Password = "",
                                                                               Name = a.Name,
                                                                               LastAccess = DateTime.Now,
                                                                               FkRole_Identifier = r.Name,
                                                                               Email = a.Email,
                                                                               Status = a.Status,
                                                                               Phone = a.Phone,
                                                                               LastChangeDate = DateTime.Now,
                                                                               FkCompanies_Identifier = a.FkCompanies_Identifier,
                                                                               FkCountries_Identifier = "",
                                                                               Address = a.Address,
                                                                               Center = CLinkedCentres.Instance.SearchCentresForUserJson(a.PkIdentifier)
                                                                           }).ToList();

                    return ListUser;
                }
                else
                    return new List<Carvajal.Turns.Domain.Entities.Users>();
            }
            catch (Exception ex)
            {
                if (ObjectUser != null)
                    LogComponent.WriteError(User.FkCompanies_Identifier, "0", "SearchUserCompany" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "SearchUserCompany" + "BGM" + ex.Message);

                return null;
            }
        }
    }
}