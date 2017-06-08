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

        /// <summary>
        /// Metodo que permite la creacion de un rol
        /// </summary>
        /// <param name="Rol">Objeto rol con la informacion con la que se creara el rol</param>
        /// <returns>true si el proceso fue exitoso caso contrario false</returns>
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

        /// <summary>
        ///  Metodo que permite la busqueda de un rol
        /// </summary>
        /// <param name="IdentificationRol">pk id del rol ha consultar</param>
        /// <returns>Elemento Rol con la informacion en caso contrario null</returns>
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

        /// <summary>
        /// Metodo para buscar los tipos de usuarios segun rol
        /// </summary>
        /// <param name="IdentificationRol">pk identificacion del rol</param>
        /// <returns>Lista de Roles con la informacion pertinente si el proceso fue exitoso en caso contrario null</returns>
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