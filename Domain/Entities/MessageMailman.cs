using System.Text;

namespace Domain.Entities
{
    public class MessageMailman
    {
        private string _from;
        private string _to;
        private string _subject;
        private StringBuilder _body;
        private bool _isBodyHtml;
        private bool _isCarbonCopy;
        private bool _isBlindCarbonCopy;

        public string From
        {
            get { return _from; }
            set { _from = value; }
        }

        public string To
        {
            get { return _to; }
            set { _to = value; }
        }

        public string Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }

        public StringBuilder Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public bool IsBodyHtml
        {
            get { return _isBodyHtml; }
            set { _isBodyHtml = value; }
        }

        public bool IsCarbonCopy
        {
            get { return _isCarbonCopy; }
            set { _isCarbonCopy = value; }
        }

        public bool IsBlindCarbonCopy
        {
            get { return _isBlindCarbonCopy; }
            set { _isBlindCarbonCopy = value; }
        }
    }
}
