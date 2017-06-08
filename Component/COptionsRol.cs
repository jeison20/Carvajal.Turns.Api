using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    public class COptionsRol : ModelData
    {
        private static COptionsRol _Instance = new COptionsRol();

        public COptionsRol()
      : base()
        {
        }


        public static COptionsRol Instance
        {
            get
            {
                return _Instance;
            }
        }

        /// <summary>
        /// Metodo para buscar la relacion de una opcion con un rol
        /// </summary>
        /// <param name="IdOption">id pk de la relacion</param>
        /// <returns>Object OptionsRol si el proceso fue exitoso en caso contrario null.</returns>
        public OptionsRol SearchOption(long IdOption)
        {
            try
            {
                return Instance.OptionsRol.FirstOrDefault(c => c.PkIdOptionRol == IdOption);
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchOption" + "BGM" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Metodo que busca la lista de opciones asociadas ha un rol especifico
        /// </summary>
        /// <param name="Rol">rol asociado a las opciones a buscar</param>
        /// <returns>Object List<Options> si el proceso fue exitoso en caso contrario lista vacia </returns>
        public List<Options> SearchOptionsForRol(string Rol)
        {
            try
            {
                List<decimal> ListOptionsRol = Instance.OptionsRol.Where(c => c.FkRol_Identifier == Rol).Select(c=>c.FkOption_Identifier).ToList();
                return COptions.Instance.SearchOptions(ListOptionsRol);

            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchOptionsForRol" + "BGM" + ex.Message);
                return new List<Options>();
            }
        }
    }
}
