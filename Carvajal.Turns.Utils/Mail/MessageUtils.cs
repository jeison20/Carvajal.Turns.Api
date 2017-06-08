using Carvajal.Turns.Utils.Interfaces;
using Carvajal.Turns.Utils.Resources;
using System;
using System.Configuration;
using System.IO;

namespace Carvajal.Turns.Utils.Mail
{
    public class MessageUtils : IMessageUtils
    {
        /// <summary>
        /// Metodo que realiza la busqueda de la plantilla html MailRecoveryPasswor para la recuperacion de contraseña almacenada en un recurso para su uso en un email
        /// </summary>
        /// <param name="userName">nombre del usuario </param>
        /// <param name="codeRecovery">codigo de recuperacion de contraseña</param>
        /// <returns>retorna un html</returns>
        public string GetTemplateMailRecoveryPasswor(string userName, string codeRecovery)
        {
            try
            {
                var messageTemplate = ResourceUtils.RestorePassword;
                messageTemplate = messageTemplate.Replace("<%UserName%>", userName);
                messageTemplate = messageTemplate.Replace("<%CodeRecovery%>", codeRecovery);
                return messageTemplate;
            }
            catch (Exception ex)
            {
                // TODO: Pendiente implementar log para control de execpciones.
                return null;
            }
        }

        /// <summary>
        /// Metodo que realiza la busqueda de la plantilla html ChangeMail para recordatorio de usuario y contraseña al cambiar de email almacenada en un recurso para su uso en un email
        /// </summary>
        /// <param name="UserName">usuario identificacion</param>
        /// <param name="Password">contraseña almacenada</param>
        /// <returns>Retorna un html</returns>
        public string GetTemplateChangeMail(string UserName, string Password)
        {
            try
            {
                var messageTemplate = ResourceUtils.SendUserPassword;
                messageTemplate = messageTemplate.Replace("<%UserName%>", UserName);
                messageTemplate = messageTemplate.Replace("<%Password%>", Password);
                return messageTemplate;
            }
            catch (Exception ex)
            {
                // TODO: Pendiente implementar log para control de execpciones.
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="merchantName"></param>
        /// <param name="manufacturerName"></param>
        /// <param name="codeRecovery"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        public string GetTemplateMailCreateMerchantInitial(string merchantName, string manufacturerName, string codeRecovery, string country)
        {
            try
            {
                TextReader template = new StreamReader(@Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\MerchantInitialMail.txt"));

                var messageTemplate = template.ReadToEnd();
                template.Close();
                messageTemplate = messageTemplate.Replace("<%MerchantName%>", merchantName);
                messageTemplate = messageTemplate.Replace("<%ManufacturerName%>", manufacturerName);
                messageTemplate = messageTemplate.Replace("<%UrlMerchantInitial%>", string.Format(ConfigurationManager.AppSettings["UrlMerchantInitial"], codeRecovery, country));
                messageTemplate = messageTemplate.Replace("<%UUID%>", codeRecovery.ToString());
                return messageTemplate;
            }
            catch (Exception ex)
            {
                // TODO: Pendiente implementar log para control de execpciones.
                return null;
            }
        }
    }
}