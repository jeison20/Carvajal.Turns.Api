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
        /// <summary>
        /// Metodo que genera una cadena de caracteres aleatoria
        /// </summary>
        /// <param name="length"></param>
        /// <returns>retorna una cadena de caracteres aleatoria</returns>
        public string RandomString(int length)
        {
            var random = new Random();
            var chars = ConfigurationManager.AppSettings["charCodeRecovery"];
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// metodo que consulta un recurso y retorna el valor
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>una cadena de caracteres con el valor del recurso consultado</returns>
        public string GetResourceMessages(string Key)
        {
            return ResourceMessages.ResourceManager.GetString(Key);
        }
    }
}
