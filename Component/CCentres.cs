using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Component
{
    public class CCentres : ModelData
    {
        private static CCentres _Instance = new CCentres();

        public CCentres()
      : base()
        {
        }

        public static CCentres Instance
        {
            get
            {
                return _Instance;
            }
        }

        public bool SaveCenter(Centres Center)
        {
            try
            {
                Centres.Add(Center);
                Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SaveCenter" + "BGM" + ex.Message);
                return false;
            }
        }

        public Centres SearchCenter(string IdentificationNumber)
        {
            Centres ObjectCenter = new Centres();
            try
            {
                ObjectCenter = Instance.Centres.FirstOrDefault(c => c.PkIdentifier == IdentificationNumber);
                return ObjectCenter;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchCenter" + "BGM" + ex.Message);

                return null;
            }
        }
        public Centres SearchCenterResponsibleUser(string IdentificationNumberUser)
        {
            Centres ObjectCenter = new Centres();
            try
            {
                ObjectCenter = Instance.Centres.FirstOrDefault(c => c.FkUsers_Responsable_Identifier == IdentificationNumberUser);
                return ObjectCenter;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchCenterResponsibleUser" + "BGM" + ex.Message);

                return null;
            }
        }
    

        public bool DeleteCenter(Centres Center)
        {
            try
            {
                if (Centres.FirstOrDefault(c => c.PkIdentifier.Equals(Center.PkIdentifier)) != null)
                {
                    Centres.Remove(Center);
                    Instance.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "DeleteCenter" + "BGM" + ex.Message);
                return false;
            }
        }

        public bool UpdateCenter(Centres Center)
        {
            Centres UpdateCenter = new Centres();
            try
            {
                UpdateCenter = Centres.FirstOrDefault(c => c.PkIdentifier.Equals(Center.PkIdentifier));
                if (UpdateCenter != null)
                {
                    UpdateCenter.FkUsers_Merchant_Identifier = Center.FkUsers_Merchant_Identifier;
                    UpdateCenter.FkUsers_Responsable_Identifier = Center.FkUsers_Responsable_Identifier;
                    UpdateCenter.WeeklyCapacity = Center.WeeklyCapacity;
                    UpdateCenter.CurrentWeekCapacity = Center.CurrentWeekCapacity;
                    UpdateCenter.FirstDay = Center.FirstDay;
                    UpdateCenter.ListOfWorkingDays = Center.ListOfWorkingDays;
                    UpdateCenter.StartTime = Center.StartTime;
                    UpdateCenter.EndTime = Center.EndTime;
                    UpdateCenter.Name = Center.Name;
                    UpdateCenter.NumberOfDocks = Center.NumberOfDocks;
                    UpdateCenter.TimeBetweenSuppliers = Center.TimeBetweenSuppliers;
                    UpdateCenter.Status = Center.Status;
                    UpdateCenter.LastChangeDate = Center.LastChangeDate;
                    UpdateCenter.FkTimezones_Identifier = Center.FkTimezones_Identifier;
                    UpdateCenter.AddressStreet = Center.AddressStreet;
                    UpdateCenter.AddressNumber = Center.AddressNumber;
                    UpdateCenter.PostCode = Center.PostCode;
                    UpdateCenter.Town = Center.Town;
                    UpdateCenter.Region = Center.Region;
                    Instance.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "UpdateCenter" + "BGM" + ex.Message);
                return false;
            }
        }
    }
}