using System;
using Umi.Wbp.Mvvm.Common;

namespace Umi.Wbp.Dialogs;

public abstract class DialogAwareAbstract : IDialogAware
{
    public virtual bool CanCloseDialog(IParameters dialogParameters) => true;

    public virtual void OnDialogClosed(){
    }

    public virtual void OnDialogOpened(IParameters parameters){
    }

    public virtual string Title => nameof(Title);

    public event Action<IDialogResult> RequestClose;

    protected void RequestDialogClose(DialogResult dialogResult = null){
        RequestClose?.Invoke(dialogResult);
    }
}