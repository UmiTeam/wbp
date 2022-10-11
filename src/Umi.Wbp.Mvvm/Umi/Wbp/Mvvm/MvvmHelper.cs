using System;
using System.Windows;

namespace Umi.Wbp.Mvvm;

public class MvvmHelper
{
    public static void CallViewAndViewModelAction<T>(object view, Action<T> action) where T : class{
        if (view is T viewAsT)
            action(viewAsT);

        if (view is FrameworkElement { DataContext: T viewModelAsT }){
            action(viewModelAsT);
        }
    }
}