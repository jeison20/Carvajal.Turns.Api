using Carvajal.Turns.Utils.Security;
using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

        /// <summary>
        /// Metodo que guarda un objecto Client
        /// </summary>
        /// <param name="ObjectClient">objecto Client que se va ha crear</param>
        /// <returns>Retorna true si la operacion fue exitosa en caso contrario false</returns>
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
                LogComponent.WriteError(ObjectClient.User, "0", "SaveClient" + "BGM" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Metodo que busca un objecto Cliente
        /// </summary>
        /// <param name="Token">token suministrado para buscarlo entre los tokens almacenados</param>
        /// <returns>Retorna un objecto Client si la busqueda fue exitosa en caso contrario null</returns>
        public Client SearchClient(string Token)
        {
            try
            {
                return Instance.Client.FirstOrDefault(c => c.Token == Token && c.Active && c.RefreshTokenLifeTime > DateTime.Now);
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchClient" + "BGM" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Metodo encargado de elminar un objeto Client de la bd
        /// </summary>
        /// <param name="ObjectClient">objeto Client que se desea eliminar de la bd</param>
        /// <returns>Retorna true si la operacion fue exitosa en caso contrario false</returns>
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
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "DeleteClient" + "BGM" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Metodo encargado de actualizar un objeto Client de la bd
        /// </summary>
        /// <param name="ObjectClient">objeto Client que se desea actualizar de la bd</param>
        /// <returns>Retorna true si la operacion fue exitosa en caso contrario false</returns>
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
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "UpdateClient" + "BGM" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Metodo encargado de identificar si el token es valido
        /// </summary>
        /// <param name="Token">token suministrado</param>
        /// <param name="User">usuario dueño del token</param>
        /// <returns>Retorna true si la operacion fue exitosa en caso contrario false</returns>
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
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "ValidToken" + "BGM" + ex.Message);
                User = "";
                return false;
            }
        }

        /// <summary>
        /// Metodo encargado de identificar si el token es valido
        /// </summary>
        /// <param name="Token">token suministrado</param>
        /// <returns>Retorna true si la operacion fue exitosa en caso contrario false</returns>
        public bool ValidToken(string Token)
        {
            try
            {
                Token = DecodeToken(Token);

                string ValidaToken = Encode(Token);
                Client Client = SearchClient(Token, ValidaToken);
                if (Client != null)
                {
                    Client.RefreshTokenLifeTime = (DateTime.Now.AddMinutes(5));
                    Instance.UpdateClient(Client);
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "ValidToken" + "BGM" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Metodo encargado de desactivar el ultimo token activo
        /// </summary>
        /// <param name="token">token suministrado</param>
        /// <param name="ValidOperation">Retorna true si la operacion fue exitosa en caso contrario false</param>
        /// <returns>mensaje con el resultado de la operacion</returns>
        public string InactiveToken(string token, out bool ValidOperation)
        {
            try
            {
                token = DecodeToken(token);
                List<Client> Tokens = new List<Client>();
                Tokens = Instance.Client.Where(c => c.Token == token && c.Active).ToList();
                ValidOperation = false;
                if (Tokens.Count == 0)
                    return "No active session was found";
                foreach (var item in Tokens)
                {
                    item.Active = false;
                    ValidOperation = Instance.UpdateClient(item);
                }

                return ValidOperation ? new Utils().GetResourceMessages("M17") : new Utils().GetResourceMessages("M12");
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "InactiveToken" + "BGM" + ex.Message);
                ValidOperation = false;
                return new Utils().GetResourceMessages("M12");
            }
        }

        /// <summary>
        ///Metodo que busca multiples tokens activos
        /// </summary>
        /// <param name="User">usuario asociado al token</param>
        public void InactiveTokenVigentes(string User)
        {
            try
            {
                List<Client> Tokens = new List<Client>();
                Tokens = Instance.Client.Where(c => c.User == User && c.Active).ToList();

                foreach (var item in Tokens)
                {
                    item.Active = false;
                    Instance.UpdateClient(item);
                }
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "InactiveTokenVigentes" + "BGM" + ex.Message);
            }
        }

        /// <summary>
        ///Metodo que realiza la busqueda de un token vigente activo
        /// </summary>
        /// <param name="Token">Codigo token suministrado</param>
        /// <param name="ComfirmaToken">Cadena de validacion de integridad del token suministrado </param>
        /// <returns>Object Client si la busqueda fue exitosa en caso contrario null</returns>
        public Client SearchClient(string Token, string ComfirmaToken)
        {
            try
            {
                Client Dato = Instance.Client.FirstOrDefault(c => c.Token.Equals(Token) && c.AllowedOrigin.Equals(ComfirmaToken) && c.Active && c.RefreshTokenLifeTime > DateTime.Now);
                return Dato;
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchClient" + "BGM" + ex.Message);
                return null;
            }
        }

        /// <summary>
        ///Metodo que retorna la razon por la cual el token suministrado no paso la validacion
        /// </summary>
        /// <param name="Token">Codigo token suministrado</param>
        /// <returns>Mensaje especificando la razon por la que el token no es valido </returns>
        public string SearchDetailInvalidToken(string Token)
        {
            try
            {
                Token = DecodeToken(Token);

                string ValidToken = Encode(Token);
                Client Data = Instance.Client.FirstOrDefault(c => c.Token.Equals(Token));
                if (Data == null)
                {
                    LogComponent.WriteLog("0", "0", "SearchDetailInvalidToken" + "BGM" + "Token no encontrado en la bd");
                    return "Not access";
                }
                else if (Data.AllowedOrigin.Equals(ValidToken))
                {
                    LogComponent.WriteLog("0", "0", "SearchDetailInvalidToken" + "BGM" + "Token modificado la cadena de verificacion no coincide");
                    return "Not access";
                }
                else if (!Data.Active)
                    return "Session Inactiva";
                else if (Data.RefreshTokenLifeTime < DateTime.Now)
                    return "el tiempo de su session ha caducado";

                return "Invalid Access";
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchDetailInvalidToken" + "BGM" + ex.Message);
                return new Utils().GetResourceMessages("M12");
            }
        }

        /// <summary>
        ///Metodo encargado de buscar el usuario asociado a un token especifico
        /// </summary>
        /// <param name="Token">Codigo token suministrado</param>
        /// <returns>en caso exitoso identificacion del usuario asociado al token en caso contrario null o vacio.</returns>
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
                return null;
            }
        }

        /// <summary>
        ///Metodo que genera codigo de autenticidad del token generado
        /// </summary>
        /// <param name="Token">Codigo token suministrado</param>
        /// <returns>Codigo autenticidad del token suministrado</returns>
        public string Encode(string Token)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(Token));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        /// <summary>
        ///Metodo que decodifica el codigo token suministrado
        /// </summary>
        /// <param name="Token">Codigo token suministrado</param>
        /// <returns>Codigo token decodificado </returns>
        private string DecodeToken(string Token)
        {
            var base64EncodedBytes = Convert.FromBase64String(Token);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}