using Umi.Wbp.Core.Common;

namespace Umi.Wbp.Dialogs
{
    /// <summary>
    /// Contains <see cref="IDialogParameters"/> from the dialog
    /// and the <see cref="ButtonResult"/> of the dialog.
    /// </summary>
    public interface IDialogResult
    {
        /// <summary>
        /// The parameters from the dialog.
        /// </summary>
        IParameters Parameters { get; }

        /// <summary>
        /// The result of the dialog.
        /// </summary>
        ButtonResult Result { get; }
    }
}
