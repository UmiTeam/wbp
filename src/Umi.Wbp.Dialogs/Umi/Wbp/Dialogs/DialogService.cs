using Microsoft.Extensions.DependencyInjection;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using JetBrains.Annotations;
using Ookii.Dialogs.Wpf;
using Umi.Wbp.Mvvm.Common;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Dialogs
{
    /// <summary>
    /// Implements <see cref="IDialogService"/> to show modal and non-modal dialogs.
    /// </summary>
    public class DialogService : IDialogService, ITransientDependency
    {
        private readonly IServiceProvider serviceProvider;

        public DialogService(IServiceProvider serviceProvider){
            this.serviceProvider = serviceProvider;
        }

        public void Show<T>(IParameters parameters = null, Action<IDialogResult> callback = null) where T : FrameworkElement{
            ShowDialogInternal(typeof(T), parameters, callback, false);
        }

        public void Show<V, W>(IParameters parameters = null, Action<IDialogResult> callback = null) where V : FrameworkElement where W : IDialogWindow{
            ShowDialogInternal(typeof(V), parameters, callback, false, typeof(W).Name);
        }

        public void ShowDialog<T>(IParameters parameters = null, Action<IDialogResult> callback = null) where T : FrameworkElement{
            ShowDialogInternal(typeof(T), parameters, callback, true);
        }

        public void ShowDialog<V, W>(IParameters parameters = null, Action<IDialogResult> callback = null) where V : FrameworkElement where W : IDialogWindow{
            ShowDialogInternal(typeof(V), parameters, callback, true, typeof(W).Name);
        }

        public void ShowFileDialog(VistaFileDialog vistaFileDialog, Action<IDialogResult> callback = null){
            Parameters dialogParameters = new();
            DialogResult dialogResult;
            if (vistaFileDialog.ShowDialog() ?? false){
                dialogParameters.Add(DialogResultExtensions.FilePathsKey, vistaFileDialog.FileNames);
                dialogResult = new(ButtonResult.Yes, dialogParameters);
            }
            else{
                dialogResult = new(ButtonResult.No, dialogParameters);
            }

            callback?.Invoke(dialogResult);
        }

        public void ShowFolderBrowserDialog(VistaFolderBrowserDialog dialog, Action<IDialogResult> callback = null){
            Parameters dialogParameters = new();
            DialogResult dialogResult;
            if (dialog.ShowDialog() ?? false){
                dialogParameters.Add(DialogResultExtensions.FolderPathsKey, dialog.SelectedPaths);
                dialogResult = new(ButtonResult.Yes, dialogParameters);
            }
            else{
                dialogResult = new(ButtonResult.No, dialogParameters);
            }

            callback?.Invoke(dialogResult);
        }

        public void ShowProgressDialog(ProgressDialog dialog, Action<IDialogResult> callback = null){
            dialog.RunWorkerCompleted += (sender, args) =>
            {
                DialogResult dialogResult;
                Parameters dialogParameters = new();
                if (args.Cancelled || args.Error != null){
                    dialogParameters.Add(DialogResultExtensions.ProgressErrorKey, args.Error);
                    if (args.Cancelled){
                        dialogResult = new(ButtonResult.Cancel, dialogParameters);
                    }
                    else{
                        dialogResult = new(ButtonResult.No, dialogParameters);
                    }
                }
                else{
                    dialogParameters.Add(DialogResultExtensions.ProgressResultKey, args.Result);
                    dialogResult = new(ButtonResult.Yes, dialogParameters);
                }

                callback?.Invoke(dialogResult);
            };
            dialog.ShowDialog();
        }

        void ShowDialogInternal(Type contentType, IParameters parameters, Action<IDialogResult> callback, bool isModal, string windowName = null){
            if (parameters == null) parameters = new Parameters();

            var dialogWindow = CreateDialogWindow(windowName);
            ConfigureDialogWindowEvents(dialogWindow, callback, parameters);
            ConfigureDialogWindowContent(contentType, dialogWindow, parameters);

            ShowDialogWindow(dialogWindow, isModal);
        }

        protected virtual void ShowDialogWindow(IDialogWindow dialogWindow, bool isModal){
            if (isModal)
                dialogWindow.ShowDialog();
            else
                dialogWindow.Show();
        }

        protected virtual IDialogWindow CreateDialogWindow(string name){
            if (string.IsNullOrWhiteSpace(name)) name = nameof(DialogWindow);
            foreach (var dialogWindow in serviceProvider.GetServices<IDialogWindow>()){
                if (dialogWindow.GetType().Name == name){
                    return dialogWindow;
                }
            }

            return serviceProvider.GetRequiredService<IDialogWindow>();
        }


        protected virtual void ConfigureDialogWindowContent(Type contentType, IDialogWindow window, IParameters parameters){
            var content = serviceProvider.GetRequiredService(contentType);
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            // if (!(dialogContent.DataContext is IDialogAware viewModel))
            //     throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            ConfigureDialogWindowProperties(window, dialogContent, dialogContent.DataContext);

            if (dialogContent.DataContext is IDialogAware dialogAware){
                dialogAware.OnDialogOpened(parameters);
            }
        }


        protected virtual void ConfigureDialogWindowEvents(IDialogWindow dialogWindow, Action<IDialogResult> callback, IParameters dialogParameters){
            Action<IDialogResult> requestCloseHandler = null;
            requestCloseHandler = (o) =>
            {
                dialogWindow.Result = o;
                dialogWindow.Close();
            };

            RoutedEventHandler loadedHandler = null;
            loadedHandler = (o, e) =>
            {
                dialogWindow.Loaded -= loadedHandler;
                if (dialogWindow.DataContext is IDialogAware dialogAware){
                    dialogAware.RequestClose += requestCloseHandler;
                }
                // dialogWindow.GetDialogViewModel().RequestClose += requestCloseHandler;
            };
            dialogWindow.Loaded += loadedHandler;

            CancelEventHandler closingHandler = null;
            closingHandler = (o, e) =>
            {
                if (dialogWindow.DataContext is IDialogAware dialogAware && !dialogAware.CanCloseDialog(dialogParameters)) e.Cancel = true;
            };
            dialogWindow.Closing += closingHandler;

            EventHandler closedHandler = null;
            closedHandler = (o, e) =>
            {
                dialogWindow.Closed -= closedHandler;
                dialogWindow.Closing -= closingHandler;
                if (dialogWindow.DataContext is IDialogAware dialogAware){
                    dialogAware.RequestClose -= requestCloseHandler;
                    dialogAware.OnDialogClosed();
                }
                // dialogWindow.GetDialogViewModel().RequestClose -= requestCloseHandler;

                // dialogWindow.GetDialogViewModel().OnDialogClosed();

                if (dialogWindow.Result == null)
                    dialogWindow.Result = new DialogResult();

                callback?.Invoke(dialogWindow.Result);

                dialogWindow.DataContext = null;
                dialogWindow.Content = null;
            };
            dialogWindow.Closed += closedHandler;
        }


        protected virtual void ConfigureDialogWindowProperties(IDialogWindow window, FrameworkElement dialogContent, [CanBeNull] object viewModel){
            var windowStyle = Dialog.GetWindowStyle(dialogContent);
            if (windowStyle != null)
                window.Style = windowStyle;

            window.Content = dialogContent;
            window.DataContext = viewModel;

            if (window.Owner == null)
                window.Owner = Application.Current?.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
        }
    }
}