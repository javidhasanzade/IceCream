using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace IceCream.ViewModels;

public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged<T>(out T prop, T value, [CallerMemberName] string? propertyName = null)
    {
        prop = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}