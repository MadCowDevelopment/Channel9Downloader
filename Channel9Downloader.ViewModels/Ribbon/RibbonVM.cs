using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;

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
        /// <param name="ribbonFactory">The ribbon factory used for creating ribbon items.</param>
        [ImportingConstructor]
        public RibbonVM(IRibbonFactory ribbonFactory)
        {
            var tabs = ribbonFactory.CreateRibbonTabs();
            Tabs = new ObservableCollection<IRibbonTabVM>(tabs);
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
    }
}