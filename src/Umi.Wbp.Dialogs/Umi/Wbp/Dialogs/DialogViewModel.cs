using System;

namespace Umi.Wbp.Dialogs;

public abstract class DialogViewModel : IDialogAware
{
    public virtual bool CanCloseDialog(IDialogParameters dialogParameters) => true;

    public virtual void OnDialogClosed(){
    }

    public virtual void OnDialogOpened(IDialogParameters parameters){
    }

    public virtual string Title { get; } = "Title";
    public event Action<IDialogResult> RequestClose;

    protected void RaiseRequestClose(IDialogResult dialogResult){
        RequestClose?.Invoke(dialogResult);
    }
}