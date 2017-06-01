using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Carvajal.Turns.Domain.Entities
{
    public class Users
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
        private string _Center;

        [JsonProperty(PropertyName = "PkIdentifier")]
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


        [JsonProperty(PropertyName = "FkRole_Identifier")]
        public string FkRole_Identifier
        {
            get { return _FkRole_Identifier; }
            set { _FkRole_Identifier = value; }
        }

        [Required]
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

        [JsonProperty(PropertyName = "FkCompanies_Identifier")]
        public string FkCompanies_Identifier
        {
            get { return _FkCompanies_Identifier; }
            set { _FkCompanies_Identifier = value; }
        }


        [JsonProperty(PropertyName = "FkCountries_Identifier")]
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

        [JsonProperty(PropertyName = "Center")]
        public string Center
        {
            get { return _Center; }
            set { _Center = value; }
        }

    }
}
