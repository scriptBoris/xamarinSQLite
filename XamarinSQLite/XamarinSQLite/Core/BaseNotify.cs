using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinSQLite.Core
{
    public abstract class BaseNotify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(object invoker, string propertyName)
        {
            PropertyChanged?.Invoke(invoker, new PropertyChangedEventArgs(propertyName));
        }
    }
}
