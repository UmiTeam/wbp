
/* 项目“Umi.Wbp.Dialogs (net5.0-windows)”的未合并的更改
在此之前:
using System;
在此之后:
using Ookii.Dialogs.Wpf;
using System;
*/
using Ookii.Dialogs.Wpf;
using System;
using System.ComponentModel;
using System.Windows;
using Umi.Wbp.Dialogs;
using Volo.Abp;

namespace Umi.Wbp.Core.Common.Dialogs
{
    /// <summary>
    /// Extensions for the IDialogService
    /// </summary>
    public static class IDialogServiceExtensions
    {
        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        public static void Show<T>(this IDialogService dialogService) where T : FrameworkElement
        {
            dialogService.Show<T>(new DialogParameters(), null);
        }

        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        public static void Show<T>(this IDialogService dialogService, Action<IDialogResult> callback) where T : FrameworkElement
        {
            dialogService.Show<T>(new DialogParameters(), callback);
        }

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        public static void ShowDialog<T>(this IDialogService dialogService) where T : FrameworkElement
        {
            dialogService.ShowDialog<T>(new DialogParameters(), null);
        }

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        public static void ShowDialog<T>(this IDialogService dialogService, Type contentType, Action<IDialogResult> callback) where T : FrameworkElement
        {
            dialogService.ShowDialog<T>(new DialogParameters(), callback);
        }

        private static IDialogResult ShowVistaFileDialogInternal(VistaFileDialog vistaFileDialog)
        {
            if (!VistaFileDialog.IsVistaFileDialogSupported) throw new BusinessException("Because you are not using Windows Vista or later, the regular open file dialog will be used. Please use Windows Vista to see the new dialog.");
            ButtonResult buttonResult = vistaFileDialog.ShowDialog() ?? false ? ButtonResult.Yes : ButtonResult.No;
            DialogParameters dialogParameters = new();
            if (buttonResult == ButtonResult.Yes) dialogParameters.Add(DialogResultExtensions.FilePathsKey, vistaFileDialog.FileNames);
            return new DialogResult(buttonResult, dialogParameters);
        }

        public static IDialogResult ShowOpenFileDialog(this IDialogService dialogService, string filter)
        {
            var dialog = new VistaOpenFileDialog
            {
                Filter = filter
            };
            return ShowVistaFileDialogInternal(dialog);
        }

        public static IDialogResult ShowSaveFileDialog(this IDialogService dialogService, string filter)
        {
            var dialog = new VistaSaveFileDialog
            {
                Filter = filter
            };
            return ShowVistaFileDialogInternal(dialog);
        }

        public static IDialogResult ShowFileDialog(this IDialogService dialogService, VistaFileDialog dialog) => ShowVistaFileDialogInternal(dialog);

        public static IDialogResult ShowFolderBrowserDialog(this IDialogService dialogService, string description = null)
        {
            var dialog = new VistaFolderBrowserDialog()
            {
                Description = description,
                UseDescriptionForTitle = true
            };
            ButtonResult buttonResult = dialog.ShowDialog() ?? false ? ButtonResult.Yes : ButtonResult.No;
            DialogParameters dialogParameters = new();
            if (buttonResult == ButtonResult.Yes) dialogParameters.Add(DialogResultExtensions.FolderPathsKey, dialog.SelectedPaths);
            return new DialogResult(buttonResult, dialogParameters);
        }

        public static void ShowProgressDialog(this IDialogService dialogService, string text, Action<ProgressDialog, ProgressDialogDoWorkEventArgs> doWork, Action<ProgressDialog, RunWorkerCompletedEventArgs> completed = null)
        {
            var dialog = new ProgressDialog()
            {
                WindowTitle = "Progressing",
                Text = text,
                Description = "Processing...",
                ShowTimeRemaining = true
            };
            dialog.RunWorkerCompleted += (sender, args) => completed?.Invoke(sender as ProgressDialog, args);
            dialog.DoWork += (sender, args) => doWork.Invoke(sender as ProgressDialog, args as ProgressDialogDoWorkEventArgs);
            dialog.ShowDialog();
        }
    }
}