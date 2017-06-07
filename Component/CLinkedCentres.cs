using Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Component
{
    public class CLinkedCentres : ModelData
    {
        private static CLinkedCentres _Instance = new CLinkedCentres();

        public CLinkedCentres()
      : base()
        {
        }

        public static CLinkedCentres Instance
        {
            get
            {
                return _Instance;
            }
        }

        public List<Centres> SearchCentresForUser(string IdentificationNumber)
        {
            List<LinkedCentres> ObjectLinkedCentres = new List<LinkedCentres>();
            List<Centres> ObjectListCentres = new List<Centres>();
            try
            {
                ObjectLinkedCentres = Instance.LinkedCentres.Where(c => c.FkUsers_Identifier == IdentificationNumber).ToList();

                foreach (var item in ObjectLinkedCentres)
                {
                    Centres ObjectCenter = CCentres.Instance.Centres.FirstOrDefault(c => c.PkIdentifier == item.FkCentres_Identifier);
                    ObjectListCentres.Add(ObjectCenter);
                }

                return ObjectListCentres;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchCentresForUser" + "BGM" + ex.Message);

                return null;
            }
        }
        public string SearchCentresForUserJson(string IdentificationNumber)
        {
            List<LinkedCentres> ObjectLinkedCentres = new List<LinkedCentres>();
            List<Centres> ObjectListCentres = new List<Centres>();
            try
            {
                ObjectLinkedCentres = Instance.LinkedCentres.Where(c => c.FkUsers_Identifier == IdentificationNumber).ToList();

                foreach (var item in ObjectLinkedCentres)
                {
                    Centres ObjectCenter = CCentres.Instance.Centres.FirstOrDefault(c => c.PkIdentifier == item.FkCentres_Identifier);
                    ObjectListCentres.Add(ObjectCenter);
                }

                string ListCentres = string.Empty;
                if (ObjectListCentres.Count > 0)
                {
                    var DataCentres = (from c in ObjectListCentres select new { id = c.PkIdentifier, name = c.Name }).ToList();
                    ListCentres = new JavaScriptSerializer().Serialize(DataCentres);
                }
                return ListCentres;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchCentresForUserJson" + "BGM" + ex.Message);

                return null;
            }
        }
        public List<Users> SearchUsersForCenter(string Identification)
        {
            List<LinkedCentres> ObjectLinkedCentres = new List<LinkedCentres>();
            List<Users> ObjectListUsers = new List<Users>();
            try
            {
                ObjectLinkedCentres = Instance.LinkedCentres.Where(c => c.FkCentres_Identifier == Identification).ToList();

                foreach (var item in ObjectLinkedCentres)
                {
                    Users ObjectUsers = CUsers.Instance.Users.FirstOrDefault(c => c.PkIdentifier == item.FkUsers_Identifier);
                    ObjectListUsers.Add(ObjectUsers);
                }

                return ObjectListUsers;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchCentresForUser" + "BGM" + ex.Message);

                return null;
            }
        }
        public bool UpdateCenterUser(string Identification, string Centres, string Role)
        {
            if (Role.Equals(ConfigurationManager.AppSettings["UserAdmin"]))
            {
                return UpdateCenterUserAdmin(Identification, Centres);
            }
            else
            {
                return UpdateCenterUserOperator(Identification, Centres);
            }

        }
        public bool UpdateCenterUserOperator(string Identification, string NewCenter)
        {
            LinkedCentres ObjectLinkedCentres = new LinkedCentres();
            try
            {
                ObjectLinkedCentres = LinkedCentres.FirstOrDefault(c => c.FkUsers_Identifier == Identification);
                ObjectLinkedCentres.FkCentres_Identifier = NewCenter;
                _Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "UpdateCenterUserOperator" + "BGM" + ex.Message);

                return false;
            }
        }
        public bool UpdateCenterUserAdmin(string Identification, string Centres)
        {
            List<LinkedCentres> ObjectLinkedCentres = new List<LinkedCentres>();
            try
            {

                ObjectLinkedCentres = Instance.LinkedCentres.Where(c => c.FkUsers_Identifier == Identification).ToList();
                foreach (var item in ObjectLinkedCentres)
                {
                    LinkedCentres Center = new LinkedCentres();
                    Center = SearchLinkedCenter(item.PkIdentifier);
                    if (Center != null)
                    {
                        LinkedCentres.Remove(Center);
                        _Instance.SaveChanges();
                    }
                }

                foreach (var item in Centres.Split(','))
                {
                    LinkedCentres.Add(new LinkedCentres { FkUsers_Identifier = Identification, FkCentres_Identifier = item });
                    _Instance.SaveChanges();
                }


                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "UpdateCenterUserAdmin" + "BGM" + ex.Message);

                return false;
            }
        }
        public LinkedCentres SearchLinkedCenter(long IdentificationNumber)
        {
            LinkedCentres ObjectLinkedCentres = new LinkedCentres();

            try
            {
                ObjectLinkedCentres = LinkedCentres.FirstOrDefault(c => c.PkIdentifier == IdentificationNumber);
                return ObjectLinkedCentres;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchLinkedCenter" + "BGM" + ex.Message);

                return null;
            }
        }
        public bool CreateLinkedCenter(LinkedCentres LinkedCenter)
        {
            try
            {
                LinkedCentres.Add(LinkedCenter);
                _Instance.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "CreateLinkedCenter" + "BGM" + ex.Message);
                return false;
            }

        }
    }
}
