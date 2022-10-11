using System;
using System.Windows;

namespace Umi.Wbp.Dialogs
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
        public static void Show<T>(this IDialogService dialogService) where T : FrameworkElement{
            dialogService.Show<T>(new DialogParameters(), null);
        }

        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        public static void Show<T>(this IDialogService dialogService, Action<IDialogResult> callback) where T : FrameworkElement{
            dialogService.Show<T>(new DialogParameters(), callback);
        }

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        public static void ShowDialog<T>(this IDialogService dialogService) where T : FrameworkElement{
            dialogService.ShowDialog<T>(new DialogParameters(), null);
        }

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        public static void ShowDialog<T>(this IDialogService dialogService, Type contentType, Action<IDialogResult> callback) where T : FrameworkElement{
            dialogService.ShowDialog<T>(new DialogParameters(), callback);
        }
    }
}