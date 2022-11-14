using System;

namespace Umi.Wbp.Routers;

public class NavigationResult
{
    public NavigationResult(NavigationContext context, bool? result){
        this.Context = context;
        this.Result = result;
    }

    public NavigationResult(NavigationContext context, Exception error){
        this.Context = context;
        this.Error = error;
        this.Result = false;
    }

    public bool? Result { get; private set; }
    public Exception Error { get; private set; }
    public NavigationContext Context { get; private set; }
}