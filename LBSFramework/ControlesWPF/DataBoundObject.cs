using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LBSFramework.ControlesWPF
{
    class DataBoundObject : INotifyPropertyChanged
    {
        private string _someString ="" ;

        public string SomeString
        {
            get { return _someString; }
            set
            {
                _someString = value;
                FirePropertyChanged("SomeString");
            }

        }


        private void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
