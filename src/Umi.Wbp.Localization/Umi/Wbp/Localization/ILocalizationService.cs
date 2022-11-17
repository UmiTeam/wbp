using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umi.Wbp.Localization
{
    public interface ILocalizationService
    {
        void ChangeCultureInfo(CultureInfo cultureInfo);
    }
}
