using System;
using System.ComponentModel;
using System.Windows;

namespace AGENT.PackageViewer.ViewModels
{
    public class ViewModelBase
    {
        //public void Attach(DependencyObject DependencyObject)
        //{
        //    // Set the changed event dispatcher.
        //    this.Dispatcher = (Key) =>
        //    {
        //        // Begin invoking of an action on the UI dispatcher.
        //        DependencyObject.Dispatcher.BeginInvoke(() =>
        //        {
        //            // Raise the changed event.
        //            this.OnPropertyChanged(Key);
        //        });
        //    };
        //}

        //private Action<string> _Dispatcher;
        //public Action<string> Dispatcher
        //{
        //    get
        //    {
        //        if (_Dispatcher == null)
        //        {
        //            return OnPropertyChanged;
        //        }
        //        return _Dispatcher;
        //    }
        //    set
        //    {
        //        _Dispatcher = value;
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
