using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Component
{
    public class CRoles : ModelData
    {
        private static CRoles _Instance = new CRoles();

        public CRoles()
          : base()
        {
        }

        public static CRoles Instance
        {
            get
            {
                return _Instance;
            }
        }

        public bool SaveRoles(Roles Rol)
        {
            try
            {
                Roles.Add(Rol);
                Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SaveRoles" + "BGM" + ex.Message);
                return false;
            }
        }

        public Roles SearchRol(string IdentificationRol)
        {
            try
            {
                return Instance.Roles.FirstOrDefault(c => c.PkIdentifier == IdentificationRol);
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchRol" + "BGM" + ex.Message);
                return null;
            }
        }

        public List<Roles> SearchTypeUserRols(string IdentificationRol)
        {
            try
            {
                if (IdentificationRol.Equals(System.Configuration.ConfigurationManager.AppSettings["UserProvider"]))
                    return Instance.Roles.Where(c => c.PkIdentifier == IdentificationRol).ToList();
                else if (IdentificationRol.Equals(System.Configuration.ConfigurationManager.AppSettings["UserAdmin"]))
                    return Instance.Roles.Where(c => c.PkIdentifier == IdentificationRol || c.PkIdentifier == System.Configuration.ConfigurationManager.AppSettings["UserOperator"] || c.PkIdentifier == System.Configuration.ConfigurationManager.AppSettings["UserProvider"]).ToList();
                else if (IdentificationRol.Equals(System.Configuration.ConfigurationManager.AppSettings["UserOperator"]))
                    return Instance.Roles.Where(c => c.PkIdentifier == IdentificationRol || c.PkIdentifier == System.Configuration.ConfigurationManager.AppSettings["UserAdmin"]).ToList();

                return Instance.Roles.Where(c => c.PkIdentifier == IdentificationRol).ToList();
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchTypeUserRols" + "BGM" + ex.Message);
                return null;
            }
        }
    }
}