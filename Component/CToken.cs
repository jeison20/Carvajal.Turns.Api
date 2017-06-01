using Carvajal.Turns.Utils.Security;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Component
{
    public class CToken : ModelData
    {
        private static CToken _Instance = new CToken();

        public CToken()
      : base()
        {
        }

        public static CToken Instance
        {
            get
            {
                return _Instance;
            }
        }

        public bool ValidToken(string Token, out string User)
        {
            try
            {
                User = string.Empty;
                Token = DecodeToken(Token);
                string ValidaToken = Encode(Token);
                Client Client = SearchClient(Token, ValidaToken);
                if (Client != null)
                {
                    User = Client.User;
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                User = "";
                return false;
            }
        }

        public bool ValidToken(string Token)
        {
            try
            {
                Token = DecodeToken(Token);

                string ValidaToken = Encode(Token);
                Client Client = SearchClient(Token, ValidaToken);
                if (Client != null)
                {
                    return true;
                }
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public string InactiveToken(string token, out bool ValidOperation)
        {
            token = DecodeToken(token);
            List<Client> Tokens = new List<Client>();
            Tokens = Instance.Client.Where(c => c.Token == token && c.Active == true).ToList();
            ValidOperation = false;
            if (Tokens.Count == 0)
                return "No active session was found";
            foreach (var item in Tokens)
            {
                item.Active = false;
                ValidOperation = CClient.Instance.UpdateClient(item);
            }

            return ValidOperation == true ? new Utils().GetResourceMessages("M17") : new Utils().GetResourceMessages("M12");
        }

        public Client SearchClient(string Token, string ComfirmaToken)
        {
            Client Dato = Instance.Client.FirstOrDefault(c => c.Token.Equals(Token) && c.AllowedOrigin.Equals(ComfirmaToken) && c.Active == true && c.RefreshTokenLifeTime > DateTime.Now);
            return Dato;
        }

        public string SearchDetailInvalidToken(string Token)
        {
            Token = DecodeToken(Token);

            string ValidToken = Encode(Token);
            Client Data = Instance.Client.FirstOrDefault(c => c.Token.Equals(Token));
            if (Data != null)
                return "Not acces";
            else if (Data.AllowedOrigin.Equals(ValidToken))
                return "Not acces";
            else if (Data.Active == false)
                return "Session Inactive";
            else if (Data.RefreshTokenLifeTime < DateTime.Now)
                return "el tiempo de su session ha caducado";

            return "Invalid Acces";
        }

        public string GetUserToken(string Token)
        {
            try
            {
                Token = DecodeToken(Token);

                string ValidaToken = Encode(Token);
                Client Client = SearchClient(Token, ValidaToken);
                if (Client != null)
                    return Client.User;
                else
                    return "";
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "GetUserToken" + "BGM" + ex.Message);
                return "";
            }
        }

        public string Encode(string Token)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(Token));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private string DecodeToken(string Token)
        {
            var base64EncodedBytes = Convert.FromBase64String(Token);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}