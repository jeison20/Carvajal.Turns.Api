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
                ObjectUser = Instance.Users.Where(c => c.FkRole_Identifier == PKRol && c.FkCompanies_Identifier == User.FkCompanies_Identifier).ToList();
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
    }
}