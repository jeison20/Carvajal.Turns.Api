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
                //LogManager.WriteLog("Error en el metodo SaveUser " + ex.Message);
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
                return null;
            }
        }

        public List<Roles> SearchTypeUserRols(string IdentificationRol)
        {
            try
            {
                if (IdentificationRol.Equals("FA"))
                    return Instance.Roles.Where(c => c.PkIdentifier == IdentificationRol).ToList();
                else if (IdentificationRol.Equals("AC"))
                    return Instance.Roles.Where(c => c.PkIdentifier == IdentificationRol || c.PkIdentifier == "OC" || c.PkIdentifier == "FA").ToList();
                else if (IdentificationRol.Equals("OC"))
                    return Instance.Roles.Where(c => c.PkIdentifier == IdentificationRol || c.PkIdentifier == "AC").ToList();

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