using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MessageFileMassive
    {
        private decimal _idLoad;
        private decimal _idCompany;
        private string _fileNameTmp;
        private string _fileName;
        private string _country;

        public decimal IdLoad
        {
            get { return _idLoad; }
            set { _idLoad = value; }
        }

        public decimal IdCompany
        {
            get { return _idCompany; }
            set { _idCompany = value; }
        }

        public string FileNameTmp
        {
            get { return _fileNameTmp; }
            set { _fileNameTmp = value; }
        }

        public string Country
        {
            get { return _country; }
            set { _country = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
    }
}
