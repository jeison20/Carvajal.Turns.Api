using System;
using System.Configuration;
using System.IO;
using Carvajal.Turns.Utils.Resources;
using Carvajal.Turns.Utils.Interfaces;

namespace Carvajal.Turns.Utils.Mail
{
    public class MessageUtils : IMessageUtils
    {
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
                throw new Exception(ex.Message);
            }
        }
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
                throw new Exception(ex.Message);
            }
        }

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
                throw new Exception(ex.Message);
            }
        }
        public string GetTemplateRequestToManufacturer(string manufacturerContact, string name)
        {
            try
            {
                TextReader template = new StreamReader(@Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\SendRequesttoManufacturer.txt"));

                var messageTemplate = template.ReadToEnd();
                template.Close();
                messageTemplate = messageTemplate.Replace("<%Manufacturer%>", manufacturerContact);
                messageTemplate = messageTemplate.Replace("<%Merchant%>", name);
                return messageTemplate;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GetTemplatePointofSalesCreate(string manufacturerContact, string name)
        {
            try
            {
                TextReader template = new StreamReader(@Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PointofSalesCreate.txt"));

                var messageTemplate = template.ReadToEnd();
                template.Close();
                messageTemplate = messageTemplate.Replace("<%contacto%>", manufacturerContact);
                messageTemplate = messageTemplate.Replace("<%empresa%>", name);
                return messageTemplate;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string GetTemplatePointofSalesVsUserCreate(string name)
        {
            try
            {
                TextReader template = new StreamReader(@Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\PointofSalesVsUserCreate.txt"));

                var messageTemplate = template.ReadToEnd();
                template.Close();
                messageTemplate = messageTemplate.Replace("<%UserName%>", name);
                return messageTemplate;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public string GetTemplateRequestToMerchant(string manufacturer, string merchant)
        {
            try
            {
                TextReader template = new StreamReader(@Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\SendRequestToMerchant.txt"));

                var messageTemplate = template.ReadToEnd();
                template.Close();
                messageTemplate = messageTemplate.Replace("<%manufacturer%>", manufacturer);
                messageTemplate = messageTemplate.Replace("<%merchant%>", merchant);
                return messageTemplate;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
