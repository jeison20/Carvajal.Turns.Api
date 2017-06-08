using Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Component
{
    internal class COptions : ModelData
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

        /// <summary>
        /// Metodo que permite crear una opcion de menu
        /// </summary>
        /// <param name="Option">Objeto Options con la informacion necesaria para la creacion</param>
        /// <returns>true si el proceso fue exitoso en caso contrario false</returns>
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
                LogComponent.WriteError("0", "0", "SaveOption" + "BGM" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Metodo para consultar un item de la tabla options
        /// </summary>
        /// <param name="IdOption">Pk id de la opcion ha consultar</param>
        /// <returns>Object Options  si el proceso fue exitoso en caso contrario null</returns>
        public Options SearchOption(int IdOption)
        {
            try
            {
                return Instance.Options.FirstOrDefault(c => c.Option_Id == IdOption);
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchOption" + "BGM" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Metodo para consultar una lista options  
        /// </summary>
        /// <param name="Options">lista de opciones a buscar </param>
        /// <returns>Object list Options si el proceso fue exitoso en caso contrario null.</returns>
        public List<Options> SearchOptions(List<decimal> Options)
        {
            try
            {
                return Instance.Options.Where(c => Options.Contains(c.Option_Id)).ToList();
            }
            catch (Exception ex)
            {
                LogComponent.WriteError("0", "0", "SearchOptions" + "BGM" + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Metodo para actualizar una opcion especifica
        /// </summary>
        /// <param name="Option">Objeto Options con la informacion correspondiente a la opcion</param>
        /// <returns>true si elproceso fue exitoso en caso contrario false</returns>
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
                LogComponent.WriteError("0", "0", "UpdateOption" + "BGM" + ex.Message);
                return false;
            }
        }
    }
}