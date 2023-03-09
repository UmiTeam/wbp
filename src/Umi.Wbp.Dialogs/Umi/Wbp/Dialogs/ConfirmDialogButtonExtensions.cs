using System;
using System.Windows;

namespace Umi.Wbp.Dialogs;

public static class ConfirmDialogButtonExtensions
{
    public static MessageBoxButton ToMessageBoxButton(this ConfirmDialogButtons confirmDialogButton){
        return confirmDialogButton switch
        {
            ConfirmDialogButtons.OK => MessageBoxButton.OK,
            ConfirmDialogButtons.YesNo => MessageBoxButton.YesNo,
            ConfirmDialogButtons.OKCancel => MessageBoxButton.OKCancel,
            ConfirmDialogButtons.YesNoCancel => MessageBoxButton.YesNoCancel,
            _ => throw new ArgumentOutOfRangeException(nameof(confirmDialogButton), confirmDialogButton, null)
        };
    }
}