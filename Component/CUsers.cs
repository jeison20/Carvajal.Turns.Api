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

        /// <summary>
        /// Metodo que permite crear un usuario
        /// </summary>
        /// <param name="User">Objeto users con la informacion pertinente al usuario a crear</param>
        /// <returns>true si el proceso fue exitoso</returns>
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
                LogComponent.WriteError("0", "0", "SaveUser" + "BGM" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Metodo para buscar un usuario por identificacion
        /// </summary>
        /// <param name="IdentificationNumber">identificacion del usuario a consultar</param>
        /// <returns>un objecto Users con la informacion del usuario consultado si el proceso fue exitoso en caso contrario null</returns>
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
                LogComponent.WriteError("0", "0", "SearchUser" + "BGM" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Metodo busca el usuario asociado ha un pk identificacion y al password
        /// </summary>
        /// <param name="User">Pk identificacion del usuario</param>
        /// <param name="Password">Contraseña establecida</param>
        /// <returns>Object users con la informacion del usuario si el proceso fue exitoso en caso contrario null.</returns>
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
                LogComponent.WriteError("0", "0", "SearchUser" + "BGM" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Metodo para buscar usuario asociado ha un email con estado activo
        /// </summary>
        /// <param name="Email">email asociado al usuario ha consultar</param>
        /// <returns>Object users con la informacion del usuario si el proceso fue exitoso en caso contrario null.</returns>
        public Users SearchUserEmail(string Email)
        {
            Users ObjectUser = new Users();
            try
            {
                ObjectUser = Instance.Users.FirstOrDefault(c => c.Email == (Email) && c.Status);
                return ObjectUser;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchUserEmail" + "BGM" + ex.Message);

                return null;
            }
        }

        /// <summary>
        /// Metodo que no exista un usuario diferente al consultado con el mismo email
        /// </summary>
        /// <param name="Email">perteneciente al usuario</param>
        /// <param name="Identification">pk identificacion del usuario</param>
        /// <returns>true si el proceso fue exitoso en caso contrario o de error false </returns>
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
                LogComponent.WriteError("0", "0", "ValidUniqueUserEmail" + "BGM" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Metodo que retorna un text aleatorio basado en la fecha
        /// </summary>
        /// <param name="LengthText">tamano del texto a retornar</param>
        /// <returns>cadena de caracteres aleatoria</returns>
        public string GetRandomText(int LengthText)
        {
            var dato = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
            string Text = Convert.ToBase64String(dato);
            if (Text.Length > LengthText)
                Text = Text.Substring(Text.Length - LengthText);
            return Text;
        }

        /// <summary>
        ///Metodo para realizar la eliminacion del usuario
        /// </summary>
        /// <param name="User">Objeto Users con la informacion del usuario</param>
        /// <returns>true si el proceso fue exitoso en caso contrario false</returns>
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
                LogComponent.WriteError("0", "0", "DeleteUser" + "BGM" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Metodo para realizar la actualizacion de usuarios
        /// </summary>
        /// <param name="User">Objeto users con la informacion del usuario </param>
        /// <returns>true si el proceso fue exitoso en caso contrario false</returns>
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
                LogComponent.WriteError("0", "0", "UpdateUser" + "BGM" + ex.Message);

                return false;
            }
        }

        /// <summary>
        /// Metodo busca la empresa a la cual se encuentra asociada un usuario
        /// </summary>
        /// <param name="User">pk identificacion del usuario</param>
        /// <returns>Pk identificacion de la empresa asociada al usuario consultado si el proceso fue exitoso en caso contrario vacio</returns>
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
                LogComponent.WriteError("0", "0", "SearchMerchantUser" + "BGM" + ex.Message);

                return "";
            }
        }

        /// <summary>
        /// Metodo para consultar los usuarios asociados a un centro
        /// </summary>
        /// <param name="User">Objeto Users con la informacion del usuario que tiene los centros asociados</param>
        /// <param name="PKRol">rol a consultar</param>
        /// <returns>Object list con los usuarios asocioados a los mismos centros del usuario suministrado en caso exitoso en caso contrario null</returns>
        public List<Users> SearchUserRolCompany(Users User, string PKRol)
        {
            List<Users> ObjectUser = new List<Users>();
            try
            {
                List<Centres> Centres = CLinkedCentres.Instance.SearchCentresForUser(User.PkIdentifier);
                if (User.FkRole_Identifier.Equals(System.Configuration.ConfigurationManager.AppSettings["UserProvider"]))
                {
                    ObjectUser = Instance.Users.Where(c => c.FkRole_Identifier == PKRol && c.FkCompanies_Identifier == User.FkCompanies_Identifier).ToList();
                }
                else if (Centres != null)
                {
                    List<Users> Users = new List<Users>();
                    foreach (var item in Centres)
                    {
                        List<Users> ListUserCenter = new List<Users>();
                        ListUserCenter = CLinkedCentres.Instance.SearchUsersForCenter(item.PkIdentifier);
                        if (Users.Count == 0)
                            Users = ListUserCenter;
                        else if (Users.Count > 0 && ListUserCenter != null && ListUserCenter.Count > 0)
                            Users.AddRange(ListUserCenter);
                    }

                    if (Users.Count > 0)
                        ObjectUser = Users;

                    ObjectUser = ObjectUser.Where(c => c.FkRole_Identifier.Equals(PKRol)).ToList();
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
                LogComponent.WriteError("0", "0", "SearchUserRolCompany" + "BGM" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Metodo para consultar los usuarios asociados a un centro
        /// </summary>
        /// <param name="User">Objeto Users con la informacion del usuario que tiene los centros asociados</param>
        /// <param name="PKRol">rol de los usuarios a consultar</param>
        /// <param name="FilterIdentification">identificacion del usuario a consultar</param>
        /// <param name="FilterName">nombre de usuario a consultar</param>
        /// <param name="Active">especificacion de si los usuarios en la busqueda deben estar en ese estado</param>
        /// <param name="Center">pk identificacion del centro a consultar</param>
        /// <returns>Objeto lista de users  con sus centros asociados a cada uno de los usuarios en caso exitoso en caso contrario null </returns>
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
                            if (ListUserCenter != null && ListUserCenter.Count > 0)
                                Users.AddRange(ListUserCenter);
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
                        ObjectUser = ObjectUser.Where(c => c.Name.Trim().ToUpper(System.Globalization.CultureInfo.InvariantCulture).Contains(FilterName.Trim().ToUpper(System.Globalization.CultureInfo.InvariantCulture))).ToList();
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
                LogComponent.WriteError("0", "0", "SearchUserCompany" + "BGM" + ex.Message);

                return null;
            }
        }
    }
}