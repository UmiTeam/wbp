using System.Linq;
using Umi.Wbp.Core.Common;

namespace Umi.Wbp.Routers;

public class NavigationParameters : ParametersBase
{
    protected bool Equals(NavigationParameters other){
        return _entries.All(other.Contains);
    }

    public override bool Equals(object obj){
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((NavigationParameters)obj);
    }

    public override int GetHashCode(){
        return _entries?.GetHashCode() ?? 0;
    }

    public static bool operator ==(NavigationParameters left, NavigationParameters right){
        return Equals(left, right);
    }

    public static bool operator !=(NavigationParameters left, NavigationParameters right){
        return !Equals(left, right);
    }

    public NavigationParameters() : base(){
    }

    public NavigationParameters(string query) : base(query){
    }
}