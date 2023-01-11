using System;
using Umi.Wbp.Core;
using Umi.Wbp.Core.Common;

namespace Umi.Wbp.Dialogs
{
    /// <summary>
    /// Interface that provides dialog functions and events to ViewModels.
    /// </summary>
    public interface IDialogAware
    {
        /// <summary>
        /// Determines if the dialog can be closed.
        /// </summary>
        /// <param name="dialogParameters">The parameters passed to the dialog.</param>
        /// <returns>If <c>true</c> the dialog can be closed. If <c>false</c> the dialog will not close.</returns>
        bool CanCloseDialog(IParameters dialogParameters);

        /// <summary>
        /// Called when the dialog is closed.
        /// </summary>
        void OnDialogClosed();

        /// <summary>
        /// Called when the dialog is opened.
        /// </summary>
        /// <param name="parameters">The parameters passed to the dialog.</param>
        void OnDialogOpened(IParameters parameters);

        /// <summary>
        /// The title of the dialog that will show in the window title bar.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Instructs the <see cref="IDialogWindow"/> to close the dialog.
        /// </summary>
        event Action<IDialogResult> RequestClose;
    }
}