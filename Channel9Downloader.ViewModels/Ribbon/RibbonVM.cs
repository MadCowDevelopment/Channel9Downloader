using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows;

using Channel9Downloader.ViewModels.Events;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Ribbon
{
    /// <summary>
    /// This class handles the ribbon viewmodel.
    /// </summary>
    [Export(typeof(IRibbonVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
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

            InitializeRibbon();
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// This event is raised when the selected ribbon tab has changed.
        /// </summary>
        public event EventHandler<SelectedTabChangedEventArgs> SelectedTabChanged;

        #endregion Events

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
                if (_selectedTab == value)
                {
                    return;
                }

                _selectedTab = value;
                OnSelectedTabChanged(new SelectedTabChangedEventArgs(_selectedTab));
            }
        }

        /// <summary>
        /// Gets the ribbon's tabs.
        /// </summary>
        public ObservableCollection<IRibbonTabVM> Tabs
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="SelectedTabChanged"/> event.
        /// </summary>
        /// <param name="e">Event args of the event.</param>
        protected void OnSelectedTabChanged(SelectedTabChangedEventArgs e)
        {
            var handler = SelectedTabChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Initializes ribbon tabs, groups and items.
        /// </summary>
        private void InitializeRibbon()
        {
            var categories = new RibbonTabVM();
            categories.Header = RibbonTabName.CATEGORIES;

            var group = new RibbonGroupVM();
            group.Header = "Some Group";

            var button = new RibbonButtonVM();
            button.Command = new RelayCommand(p => MessageBox.Show("Nice"));
            button.Label = "Show";
            button.LargeImageSource = @"..\Images\Ribbon\LargeIcon.png";
            button.ToolTipDescription = "ToolTipDescription";
            button.ToolTipTitle = "ToolTipTitle";
            group.Items.Add(button);

            categories.Groups.Add(group);
            Tabs.Add(categories);
            Tabs.Add(new RibbonTabVM { Header = RibbonTabName.DOWNLOADS });
        }

        #endregion Private Methods
    }
}