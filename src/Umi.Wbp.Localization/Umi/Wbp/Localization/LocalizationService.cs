using System.Globalization;
using System.Threading;
using Volo.Abp.DependencyInjection;

namespace Umi.Wbp.Localization
{
    public class LocalizationService : ILocalizationService, ITransientDependency
    {
        public void ChangeCultureInfo(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
            WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture = cultureInfo;
        }
    }
}
