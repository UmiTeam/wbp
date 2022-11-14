using Ookii.Dialogs.Wpf;
using System;
using Umi.Wbp.Dialogs;

namespace Umi.Wbp.Core.Common.Dialogs
{
    /// <summary>
    /// Extensions for the IDialogService
    /// </summary>
    public static class IDialogServiceExtensions
    {
        public static void ShowOpenFileDialog(this IDialogService dialogService, string filter, Action<IDialogResult> callback = null){
            VistaOpenFileDialog dialog = new()
            {
                Filter = filter
            };
            dialogService.ShowFileDialog(dialog, callback);
        }

        public static void ShowSaveFileDialog(this IDialogService dialogService, string filter, Action<IDialogResult> callback = null){
            VistaSaveFileDialog dialog = new()
            {
                Filter = filter
            };
            dialogService.ShowFileDialog(dialog, callback);
        }

        public static void ShowFolderBrowserDialog(this IDialogService dialogService, string description, Action<IDialogResult> callback = null){
            var dialog = new VistaFolderBrowserDialog()
            {
                Description = description,
            };
            dialogService.ShowFolderBrowserDialog(dialog, callback);
        }

        public static void ShowProgressDialog(this IDialogService dialogService, string description, Action<ProgressDialog, ProgressDialogDoWorkEventArgs> doWork, Action<IDialogResult> callback = null){
            var dialog = new ProgressDialog()
            {
                Text = description,
                ShowTimeRemaining = true,
                ShowCancelButton = false
            };
            dialog.DoWork += (sender, args) => doWork.Invoke(sender as ProgressDialog, args as ProgressDialogDoWorkEventArgs);
            dialogService.ShowProgressDialog(dialog, callback);
        }
    }
}