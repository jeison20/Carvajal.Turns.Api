using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Carvajal.Turns.Utils.Interfaces;
using System.Web.Script.Serialization;
using Carvajal.Turns.Utils.Resources;

namespace Carvajal.Turns.Utils.Security
{
    public class Utils : IUtils
    {
        private Dictionary<string, string> _RegexByCountry;

        public Dictionary<string, string> RegexByCountry
        {
            get { return _RegexByCountry; }
            set { _RegexByCountry = value; }
        }

        public string RandomString(int length)
        {
            var random = new Random();
            var chars = ConfigurationManager.AppSettings["charCodeRecovery"];
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public Guid GenereteUuid()
        {
            return Guid.NewGuid();
        }

        public object GetResponseMockup(string fileName)
        {
            try
            {
                var jsonMockup = new object();
                TextReader mockupReader = new StreamReader(@Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Mockup\\" + fileName));
                var mockup = mockupReader.ReadToEnd();
                var serializer = new JavaScriptSerializer();
                var obje = serializer.Deserialize(mockup, jsonMockup.GetType());
                return obje;
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public string GetRegexByCountry(string country)
        {
            return RegexByCountry.ContainsKey(country) ? RegexByCountry[country] : null;
        }

        public string GetResourceMessages(string Key)
        {
            return ResourceMessages.ResourceManager.GetString(Key);
        }
    }
}
