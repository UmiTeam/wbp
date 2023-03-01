using System;
using System.Windows;

namespace Umi.Wbp.Mvvm;

public class MvvmHelper
{
    public static void CallViewAndViewModelAction<T>(object view, Action<T> action) where T : class{
        if (view is T viewAsT)
            action(viewAsT);

        if (view is FrameworkElement { DataContext: T viewModelAsT } and not IViewModelForSelf){
            action(viewModelAsT);
        }
    }

    public static bool CallViewAndViewModelAction<T>(object view, Func<T, bool> action) where T : class{
        if (view is T viewAsT)
            if (!action(viewAsT))
                return false;

        if (view is FrameworkElement { DataContext: T viewModelAsT } and not IViewModelForSelf){
            if (!action(viewModelAsT))
                return false;
        }

        return true;
    }
}