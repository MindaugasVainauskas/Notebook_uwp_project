using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LittleNotebook.ViewModels
{
    //This helper class helps with propery chage notifications by saving code.
    //Adapted from MVVM lab code as it is a very efficient class for what I want it to do.
    public class NotificationHelper : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // SetField (Name, value); // where there is a data member
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string property
           = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            RaisePropertyChanged(property);
            return true;
        }

        // SetField(()=> somewhere.Name = value; somewhere.Name, value) 
        // Advanced case where you rely on another property
        protected bool SetProperty<T>(T currentValue, T newValue, Action DoSet,
            [CallerMemberName] String property = null)
        {
            if (EqualityComparer<T>.Default.Equals(currentValue, newValue)) return false;
            DoSet.Invoke();
            RaisePropertyChanged(property);
            return true;
        }

        protected void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    public class NotificationHelper<T> : NotificationHelper where T : class, new()
    {
        protected T This;

        public static implicit operator T(NotificationHelper<T> thing) { return thing.This; }

        public NotificationHelper(T thing = null)
        {
            This = (thing == null) ? new T() : thing;
        }
    }
}
