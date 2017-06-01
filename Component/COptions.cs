using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component
{
    class COptions : ModelData
    {
        private static COptions _Instance = new COptions();

        public COptions()
      : base()
        {
        }

        public static COptions Instance
        {
            get
            {
                return _Instance;
            }
        }

        public bool SaveOption(Options Option)
        {
            try
            {
                Options.Add(Option);
                Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //LogManager.WriteLog("Error en el metodo SaveUser " + ex.Message);
                return false;
            }
        }

        public Options SearchOption(int IdOption)
        {
            try
            {
                return Instance.Options.FirstOrDefault(c => c.Option_Id == IdOption);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Options> SearchOptions(List<decimal> Options)
        {
            try
            {
                return Instance.Options.Where(c => Options.Contains(c.Option_Id)).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool UpdateOption(Options Option)
        {
            try
            {
                Options ObjectOption = Instance.Options.FirstOrDefault(c => c.Option_Id == Option.Option_Id);
                ObjectOption.Option_father = Option.Option_father;
                ObjectOption.Option_Level = Option.Option_Level;
                ObjectOption.Description = Option.Description;
                ObjectOption.Display_Name = Option.Display_Name;
                ObjectOption.Is_Visible = Option.Is_Visible;
                ObjectOption.Requires_Authorization = Option.Requires_Authorization;
                ObjectOption.Url = Option.Url;
                ObjectOption.Icon = Option.Icon;
                ObjectOption.Parameter = Option.Parameter;
                Instance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
