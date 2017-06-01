using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    public class CClient : ModelData
    {
        private static CClient _Instance = new CClient();
        public CClient()
      : base()
        {
        }
        public static CClient Instance
        {
            get
            {
                return _Instance;
            }
        }
        public bool SaveClient(Client ObjectClient)
        {
            try
            {
                Client.Add(ObjectClient);
                Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError(ObjectClient.User, "0", "" + "BGM" + ex.Message);
                return false;
            }
        }
        public Client SearchClient(string Token)
        {
            try
            {
                return Instance.Client.FirstOrDefault(c => c.Token == Token && c.Active == true && c.RefreshTokenLifeTime > DateTime.Now);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool DeleteClient(Client ObjectClient)
        {
            try
            {
                if (Client.FirstOrDefault(c => c.Token.Equals(ObjectClient.Token)) != null)
                {
                    Client.Remove(ObjectClient);
                    Instance.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool UpdateClient(Client ObjectClient)
        {
            try
            {
                Client OrgObjectClient = new Client();
                OrgObjectClient = Client.FirstOrDefault(c => c.Token.Equals(ObjectClient.Token));
                if (OrgObjectClient != null)
                {
                    OrgObjectClient.User = ObjectClient.User;
                    OrgObjectClient.Token = ObjectClient.Token;
                    OrgObjectClient.Active = ObjectClient.Active;
                    OrgObjectClient.RefreshTokenLifeTime = ObjectClient.RefreshTokenLifeTime;
                    OrgObjectClient.AllowedOrigin = ObjectClient.AllowedOrigin;
                    Instance.SaveChanges();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
