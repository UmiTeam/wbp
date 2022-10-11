using System;
using System.Windows;

namespace Umi.Wbp.Regions
{
    /// <summary>
    /// An entry in an IRegionNavigationJournal representing the URI navigated to.
    /// </summary>
    public interface IRegionNavigationJournalEntry
    {
        /// <summary>
        /// Gets or sets the URI.
        /// </summary>
        /// <value>The URI.</value>
        Uri Uri { get;  }

        /// <summary>
        /// Gets or sets the NavigationParameters instance.
        /// </summary>
        NavigationParameters Parameters { get;  }

        FrameworkElement View { get;  }
    }
}