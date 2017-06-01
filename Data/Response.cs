using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Response
    {
        private int _code;
        private string _message;
        private string _serverCurrentDate;
        private object _data;

        public Response()
        {
            Data = new List<object> { string.Empty };
        }
        [JsonProperty(PropertyName = "code")]
        public int Code
        {
            get { return _code; }
            set { _code = value; }
        }

        [JsonProperty(PropertyName = "message")]
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        [JsonProperty(PropertyName = "data")]
        public object Data
        {
            get { return _data; }
            set { _data = value; }
        }

        [JsonProperty(PropertyName = "server_current_date")]
        public string ServerCurrentDate
        {
            get { return _serverCurrentDate; }
            set { _serverCurrentDate = value; }
        }
    }
}
