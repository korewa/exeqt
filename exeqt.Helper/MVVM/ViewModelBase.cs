using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace exeqt.Helper.MVVM
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = default(string)) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected virtual bool SetProperty<T>(ref T setter, T value, [CallerMemberName] string propertyName = default(string))
        {
            if (EqualityComparer<T>.Default.Equals(setter, value))
                return false;

            setter = value;

            OnPropertyChanged(propertyName);

            return true;
        }
    }
}