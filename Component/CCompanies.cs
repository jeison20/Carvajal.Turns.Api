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
                if (ObjectCompany != null)
                    LogComponent.WriteError(ObjectCompany.PkIdentifier, "0", "SearchCompany" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "SearchCompany" + "BGM" + ex.Message);

                return null;
            }
        }

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
                if (Company != null)
                    LogComponent.WriteError(Company.PkIdentifier, "0", "DeleteCompany" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "DeleteCompany" + "BGM" + ex.Message);
                return false;
            }
        }

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
                if (CompanyUpdate != null)
                    LogComponent.WriteError(CompanyUpdate.PkIdentifier, "0", "UpdateCompany" + "BGM" + ex.Message);
                else
                    LogComponent.WriteError("ErrorConsultBD", "0", "UpdateCompany" + "BGM" + ex.Message);

                return false;
            }
        }
    }
}