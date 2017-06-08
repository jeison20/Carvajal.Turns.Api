using Data;
using System;
using System.Linq;

namespace Component
{
    public class CCompanies : ModelData
    {
        private static CCompanies _Instance = new CCompanies();

        public CCompanies()
      : base()
        {
        }

        public static CCompanies Instance
        {
            get
            {
                return _Instance;
            }
        }

        /// <summary>
        /// Metodo que permite crear una Company
        /// </summary>
        /// <param name="Company">Objeto Company que se desea crear</param>
        /// <returns>return true si el proceso fue exitoso en caso contrario false</returns>
        public bool SaveCompany(Companies Company)
        {
            try
            {
                Companies.Add(Company);
                Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError(Company.PkIdentifier, "0", "SaveCompany" + "BGM" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Metodo para buscar una empresa por identificacion
        /// </summary>
        /// <param name="IdentificationNumber">identificacion de la empresa a buscar</param>
        /// <returns>retorna un objeto Companies si el proceso fue exitoso en caso contrario un null.</returns>
        public Companies SearchCompany(string IdentificationNumber)
        {
            Companies ObjectCompany = new Companies();
            try
            {
                ObjectCompany = Instance.Companies.FirstOrDefault(c => c.PkIdentifier == IdentificationNumber);
                return ObjectCompany;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchCompany" + "BGM" + ex.Message);

                return null;
            }
        }

        /// <summary>
        ///Metodo para realizar la eliminacion de una empresa
        /// </summary>
        /// <param name="Company">Objeto Companies con los datos pertenecientes a la empresa a eliminar</param>
        /// <returns>Return true si la operacion fue exitosa en caso contrario false</returns>
        public bool DeleteCompany(Companies Company)
        {
            try
            {
                if (Companies.FirstOrDefault(c => c.PkIdentifier.Equals(Company.PkIdentifier)) != null)
                {
                    Companies.Remove(Company);
                    Instance.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "DeleteCompany" + "BGM" + ex.Message);
                return false;
            }
        }

        /// <summary>
        ///Metodo para realizar la actualizacion de una empresa
        /// </summary>
        /// <param name="Company">Objeto Companies que contiene la informacion de la empresa ha actualizar</param>
        /// <returns>true si el proceso fue exitoso en caso contrario false</returns>
        public bool UpdateCompany(Companies Company)
        {
            Companies CompanyUpdate = new Companies();
            try
            {
                CompanyUpdate = Companies.FirstOrDefault(c => c.PkIdentifier.Equals(Company.PkIdentifier));
                if (CompanyUpdate != null)
                {
                    CompanyUpdate.ChangePasswordNextTime = Company.ChangePasswordNextTime;
                    CompanyUpdate.Password = Company.Password;
                    CompanyUpdate.Name = Company.Name;
                    CompanyUpdate.LastAccess = Company.LastAccess;
                    CompanyUpdate.FkRole_Identifier = Company.FkRole_Identifier;
                    CompanyUpdate.Email = Company.Email;
                    CompanyUpdate.Status = Company.Status;
                    CompanyUpdate.Phone = Company.Phone;
                    CompanyUpdate.LastChangeDate = Company.LastChangeDate;
                    CompanyUpdate.Companies_Identifier = Company.Companies_Identifier;
                    CompanyUpdate.AddressStreet = Company.AddressStreet;
                    CompanyUpdate.AddressNumber = Company.AddressNumber;
                    CompanyUpdate.PostCode = Company.PostCode;
                    CompanyUpdate.Town = Company.Town;
                    CompanyUpdate.Region = Company.Region;
                    CompanyUpdate.FkCountries_Identifier = Company.FkCountries_Identifier;

                    Instance.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {              
                    LogComponent.WriteError("0", "0", "UpdateCompany" + "BGM" + ex.Message);

                return false;
            }
        }
    }
}