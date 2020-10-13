using System.ComponentModel;
using System.Text.RegularExpressions;

namespace UDP_Messenger
{
    public class Data : INotifyPropertyChanged
    {
        private readonly Regex ipRegex;

        #region INotifyPropertyChanged Members

        private bool _isChecked;
        //private bool _isEnabled;
        private string _ipaddress;
        private string _message;
        private string _receivedMessagse;

        public Data()
        {
            ipRegex = new Regex(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");
            _isChecked = true;
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    OnPropertyChanged("IsChecked");
                }
            }
        }

        public bool IsEnabled
        {
            // _isEnabled
            get { return !string.IsNullOrEmpty(_message) && !string.IsNullOrEmpty(_ipaddress) && ipRegex.IsMatch(_ipaddress); }
        }

        public string IPAddress
        {
            get { return _ipaddress; }
            set
            {
                if (_ipaddress != value)
                {
                    _ipaddress = value;
                    OnPropertyChanged("IPAddress");
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged("Message");
                    OnPropertyChanged("IsEnabled");
                }
            }
        }

        public string ReceivedMessages
        {
            get { return _receivedMessagse; }
            set
            {
                if (_receivedMessagse != value)
                {
                    _receivedMessagse = value;
                    OnPropertyChanged("ReceivedMessages");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
