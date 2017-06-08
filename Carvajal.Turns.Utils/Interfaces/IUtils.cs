using System;

namespace Carvajal.Turns.Utils.Interfaces
{
    public interface IUtils
    {
        /// <summary>
        /// Metodo que genera una cadena de caracteres aleatoria
        /// </summary>
        /// <param name="length"></param>
        /// <returns>retorna una cadena de caracteres aleatoria</returns>
        string RandomString(int length);

        /// <summary>
        /// metodo que consulta un recurso y retorna el valor
        /// </summary>
        /// <param name="Key"></param>
        /// <returns>una cadena de caracteres con el valor del recurso consultado</returns>
        string GetResourceMessages(string Key);
    }
}
