using System;

namespace Umi.Wbp.Dialogs;

public abstract class DialogAwareAbstract : IDialogAware
{
    public virtual bool CanCloseDialog(IDialogParameters dialogParameters) => true;

    public virtual void OnDialogClosed(){
    }

    public virtual void OnDialogOpened(IDialogParameters parameters){
    }

    public string Title => nameof(Title);

    public event Action<IDialogResult> RequestClose;

    protected void RequestDialogClose(DialogResult dialogResult = null){
        RequestClose?.Invoke(dialogResult);
    }
}