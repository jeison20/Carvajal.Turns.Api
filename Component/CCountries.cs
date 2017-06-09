using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    public class CCountries : ModelData
    {
        private static CCountries _Instance = new CCountries();

        public CCountries()
      : base()
        {
        }

        public static CCountries Instance
        {
            get
            {
                return _Instance;
            }
        }
    }
}
