using System;
using System.Windows;
using Autofac;
using Autofac.Core.Resolving.Pipeline;

namespace Umi.Wbp.Mvvm;

public class ViewAndViewModelResolveMiddleware : IResolveMiddleware
{
    public void Execute(ResolveRequestContext context, Action<ResolveRequestContext> next){
        next(context);
        if (context.Instance is FrameworkElement frameworkElement){
            if (context.Instance is IViewModelForSelf){
                frameworkElement.DataContext = frameworkElement;
                return;
            }

            foreach (var @interface in context.Instance.GetType().GetInterfaces()){
                if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IViewFor<>)){
                    var viewModel = context.Resolve(@interface.GetGenericArguments()[0]);
                    // Setting data context
                    frameworkElement.DataContext = viewModel;

                    // Setting view model for
                    var property = @interface.GetProperty(nameof(IViewFor<object>.ViewModel));
                    property.SetValue(context.Instance, Convert.ChangeType(viewModel, property.PropertyType));

                    // Setting view for
                    foreach (var viewModelInterfaces in viewModel.GetType().GetInterfaces()){
                        if (viewModelInterfaces.IsGenericType && viewModelInterfaces.GetGenericTypeDefinition() == typeof(IViewModelFor<>)){
                            var viewPropertyInfo = viewModelInterfaces.GetProperty(nameof(IViewModelFor<FrameworkElement>.View));
                            viewPropertyInfo.SetValue(viewModel, Convert.ChangeType(context.Instance, viewPropertyInfo.PropertyType));
                            break;
                        }
                    }

                    break;
                }
            }
        }
    }

    public PipelinePhase Phase => PipelinePhase.RegistrationPipelineStart;
}