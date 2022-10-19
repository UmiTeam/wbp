using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using Umi.Wbp.Core;

namespace Umi.Wbp.Mvvm
{
    public static class ViewModelLocator
    {
        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.RegisterAttached("ViewModel", typeof(Type), typeof(ViewModelLocator),
            new PropertyMetadata(defaultValue: null, propertyChangedCallback: ViewModelChanged));

        public static void SetViewModel(DependencyObject obj, Type value)
        {
            obj.SetValue(ViewModelProperty, value);
        }

        public static Type GetViewModel(DependencyObject obj)
        {
            return (Type)obj.GetValue(ViewModelProperty);
        }


        private static void ViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element && e.NewValue is Type type)
            {
                var viewModel = IocHelper.Services.GetRequiredService(type);
                element.DataContext = viewModel;
            }
        }
    }
}