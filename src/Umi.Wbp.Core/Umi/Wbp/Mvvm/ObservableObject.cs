using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Umi.Wbp.Mvvm;

[Obsolete("Use PropertyChanged.Fody package instead")]
public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
{
    public event PropertyChangedEventHandler PropertyChanged;
    public event PropertyChangingEventHandler PropertyChanging;


    protected virtual void OnPropertyChanged(PropertyChangedEventArgs args){
        if (args == null) throw new ArgumentNullException(nameof(args));
        PropertyChanged?.Invoke(this, args);
    }

    protected virtual void OnPropertyChanging(PropertyChangingEventArgs args){
        if (args == null) throw new ArgumentNullException(nameof(args));
        PropertyChanging?.Invoke(this, args);
    }

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null){
        OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
    }

    protected void RaisePropertyChanging([CallerMemberName] string propertyName = null){
        OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
    }

    protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null){
        if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

        RaisePropertyChanging(propertyName);

        storage = value;
        onChanged?.Invoke();
        RaisePropertyChanged(propertyName);

        return true;
    }

    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null){
        if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

        RaisePropertyChanging(propertyName);

        storage = value;
        RaisePropertyChanged(propertyName);

        return true;
    }
}