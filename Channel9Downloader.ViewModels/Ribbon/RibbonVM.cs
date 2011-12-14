using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;

using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class handles the ribbon viewmodel.
    /// </summary>
    [Export(typeof(IRibbonVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class RibbonVM : ObservableObject, IRibbonVM
    {
        #region Fields

        /// <summary>
        /// Backing field for <see cref="SelectedTab"/> property.
        /// </summary>
        private IRibbonTabVM _selectedTab;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonVM"/> class.
        /// </summary>
        public RibbonVM()
        {
            Tabs = new ObservableCollection<IRibbonTabVM>();

            var home = new RibbonTabVM();
            home.Header = "Home";

            var group = new RibbonGroupVM();
            group.Header = "Some Group";

            var button = new RibbonButtonVM();
            button.Command = new RelayCommand(p => MessageBox.Show("Nice"));
            button.Label = "Show";
            button.LargeImageSource = @"..\Images\Ribbon\LargeIcon.png";
            button.ToolTipDescription = "ToolTipDescription";
            button.ToolTipTitle = "ToolTipTitle";
            group.Items.Add(button);

            home.Groups.Add(group);
            Tabs.Add(home);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the selected tab.
        /// </summary>
        public IRibbonTabVM SelectedTab
        {
            get
            {
                return _selectedTab;
            }

            set
            {
                _selectedTab = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Gets the ribbon's tabs.
        /// </summary>
        public ObservableCollection<IRibbonTabVM> Tabs
        {
            get; private set;
        }

        #endregion Public Properties
    }
}