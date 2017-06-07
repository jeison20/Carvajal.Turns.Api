using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;

namespace WcfServiceLogin
{
    public class LoginService : ILoginService
    {

        public string Login(string User, string Password)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string tokenEndpoint = WebConfigurationManager.AppSettings["UrlApi"] + "token";
                    var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);

                    request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type","password"},
                    {"username", User},
                    {"password",Password}
                });

                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var token = response.Content.ReadAsAsync<TokenModel>().Result;
                        var plainTextBytes = Encoding.UTF8.GetBytes(token.Token);
                        string TokenEnCoding = Convert.ToBase64String(plainTextBytes);
                        return WebConfigurationManager.AppSettings["UrlSitio"] + "?msg=" + "Barer " + TokenEnCoding;
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        var token = response.Content.ReadAsAsync<TokenErrorModel>().Result;
                        return WebConfigurationManager.AppSettings["UrlSitio"] + "?msg=" + "MensajeError_" + token.ErrorDescription;
                    }
                    else
                    {
                        return WebConfigurationManager.AppSettings["UrlSitio"] + "?msg=" + "MensajeError_El proceso no se terminó satisfactoriamente. Intente nuevamente en unos segundos.";
                    }

                }
            }
            catch
            {
                return WebConfigurationManager.AppSettings["UrlSitio"] + "?msg=" + "MensajeError_El proceso no se terminó satisfactoriamente. Intente nuevamente en unos segundos.";
            }

        }

    }
    public class TokenModel
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }
    public class TokenErrorModel
    {
        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("error_description")]
        public string ErrorDescription { get; set; }
    }
}
