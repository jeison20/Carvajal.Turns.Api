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


        public OptionsRol SearchOption(long IdOption)
        {
            try
            {
                return Instance.OptionsRol.FirstOrDefault(c => c.PkIdOptionRol == IdOption);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Options> SearchOptionsForRol(string Rol)
        {
            try
            {
                List<decimal> ListOptionsRol = Instance.OptionsRol.Where(c => c.FkRol_Identifier == Rol).Select(c=>c.FkOption_Identifier).ToList();
                return COptions.Instance.SearchOptions(ListOptionsRol);

            }
            catch (Exception ex)
            {
                return new List<Options>();
            }
        }
    }
}
