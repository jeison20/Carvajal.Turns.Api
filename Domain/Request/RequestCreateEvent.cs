using Newtonsoft.Json;
using System;

namespace Carvajal.Turns.Domain.Request
{

    /// <summary>
    /// Clase utilizada para estructurar la respuesta en un servicio
    /// </summary>
    public class RequestCreateEvent
    {
        private string _User;
        private string _NewPassword;
        private string _ComfirmPassword;
        private string _Code;

        [JsonProperty(PropertyName = "usuario")]
        public string User
        {
            get { return _User; }
            set { _User = value; }
        }

        [JsonProperty(PropertyName = "nuevaclave")]
        public string NewPassword
        {
            get { return _NewPassword; }
            set { _NewPassword = value; }
        }

        [JsonProperty(PropertyName = "confirmacionclave")]
        public string ComfirmPassword
        {
            get { return _ComfirmPassword; }
            set { _ComfirmPassword = value; }
        }

        [JsonProperty(PropertyName = "codigoverificacion")]
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
    }

    /// <summary>
    /// Clase utilizada para estructurar la respuesta en un servicio
    /// </summary>
    public class RequestCreateEventUser
    {
        private string _PkIdentifier;
        private bool _ChangePasswordNextTime;
        private string _Password;
        private string _Name;
        private DateTime? _LastAccess;
        private string _FkRole_Identifier;
        private string _Email;
        private bool _Status;
        private string _Phone;
        private DateTime? _LastChangeDate;
        private string _FkCompanies_Identifier;
        private string _FkCountries_Identifier;
        private string _Address;
        private string _Centres;

        [JsonProperty(PropertyName = "Identifier")]
        public string PkIdentifier
        {
            get { return _PkIdentifier; }
            set { _PkIdentifier = value; }
        }

        [JsonProperty(PropertyName = "ChangePasswordNextTime")]
        public bool ChangePasswordNextTime
        {
            get { return _ChangePasswordNextTime; }
            set { _ChangePasswordNextTime = value; }
        }

        [JsonProperty(PropertyName = "Password")]
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        [JsonProperty(PropertyName = "Name")]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [JsonProperty(PropertyName = "LastAccess")]
        public DateTime? LastAccess
        {
            get { return _LastAccess; }
            set { _LastAccess = value; }
        }

        [JsonProperty(PropertyName = "Role")]
        public string FkRole_Identifier
        {
            get { return _FkRole_Identifier; }
            set { _FkRole_Identifier = value; }
        }

        [JsonProperty(PropertyName = "Email")]
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        [JsonProperty(PropertyName = "Status")]
        public bool Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        [JsonProperty(PropertyName = "Phone")]
        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        [JsonProperty(PropertyName = "LastChangeDate")]
        public DateTime? LastChangeDate
        {
            get { return _LastChangeDate; }
            set { _LastChangeDate = value; }
        }

        [JsonProperty(PropertyName = "Company")]
        public string FkCompanies_Identifier
        {
            get { return _FkCompanies_Identifier; }
            set { _FkCompanies_Identifier = value; }
        }

        [JsonProperty(PropertyName = "Country")]
        public string FkCountries_Identifier
        {
            get { return _FkCountries_Identifier; }
            set { _FkCountries_Identifier = value; }
        }

        [JsonProperty(PropertyName = "Address")]
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        [JsonProperty(PropertyName = "Centres")]
        public string Centres
        {
            get { return _Centres; }
            set { _Centres = value; }
        }
    }
}