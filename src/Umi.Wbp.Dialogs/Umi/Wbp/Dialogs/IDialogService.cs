using System;
using System.Windows;
using Ookii.Dialogs.Wpf;

namespace Umi.Wbp.Dialogs
{
    /// <summary>
    /// Interface to show modal and non-modal dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <typeparam name="T">The dialog content type.</typeparam>
        void Show<T>(IDialogParameters parameters = null, Action<IDialogResult> callback = null) where T : FrameworkElement;

        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <typeparam name="V">The dialog content type.</typeparam>
        /// <typeparam name="W">The dialog window type.</typeparam>
        void Show<V, W>(IDialogParameters parameters = null, Action<IDialogResult> callback = null) where V : FrameworkElement where W : IDialogWindow;

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <typeparam name="T">The dialog content type.</typeparam>
        void ShowDialog<T>(IDialogParameters parameters = null, Action<IDialogResult> callback = null) where T : FrameworkElement;

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <typeparam name="V">The dialog content type.</typeparam>
        /// <typeparam name="W">The dialog window type.</typeparam>
        void ShowDialog<V, W>(IDialogParameters parameters = null, Action<IDialogResult> callback = null) where V : FrameworkElement where W : IDialogWindow;

        void ShowFileDialog(VistaFileDialog vistaFileDialog, Action<IDialogResult> callback = null);

        void ShowFolderBrowserDialog(VistaFolderBrowserDialog dialog, Action<IDialogResult> callback = null);

        void ShowProgressDialog(ProgressDialog dialog, Action<IDialogResult> callback = null);
    }
}