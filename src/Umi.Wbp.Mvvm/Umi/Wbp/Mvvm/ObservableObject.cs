using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Umi.Wbp.Helpers;

namespace Umi.Wbp.Mvvm;

public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
{
    public event PropertyChangedEventHandler PropertyChanged;
    public event PropertyChangingEventHandler PropertyChanging;


    protected virtual void OnPropertyChanged(PropertyChangedEventArgs args){
        ArgumentNullException.ThrowIfNull(args);
        PropertyChanged?.Invoke(this, args);
    }

    protected virtual void OnPropertyChanging(PropertyChangingEventArgs args){
        ArgumentNullException.ThrowIfNull(args);
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

        if (IocHelper.ServiceCollection.GetRequiredService<IOptions<WbpMvvmOptions>>().Value.EnableNotifyPropertyChanging){
            RaisePropertyChanging(propertyName);
        }

        storage = value;
        onChanged?.Invoke();
        RaisePropertyChanged(propertyName);

        return true;
    }

    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null){
        if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

        if (IocHelper.ServiceCollection.GetRequiredService<IOptions<WbpMvvmOptions>>()?.Value.EnableNotifyPropertyChanging ?? false){
            RaisePropertyChanging(propertyName);
        }

        storage = value;
        RaisePropertyChanged(propertyName);

        return true;
    }
}