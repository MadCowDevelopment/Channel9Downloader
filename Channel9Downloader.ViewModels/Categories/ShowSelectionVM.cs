using System;
using System.ComponentModel.Composition;
using System.Windows.Data;

using Channel9Downloader.DataAccess;
using Channel9Downloader.ViewModels.Framework;

namespace Channel9Downloader.ViewModels.Categories
{
    /// <summary>
    /// This class manages the show selection view.
    /// </summary>
    [Export(typeof(IShowSelectionVM))]
    public class ShowSelectionVM : BaseViewModel, IShowSelectionVM
    {
        #region Fields

        /// <summary>
        /// The category browser.
        /// </summary>
        private readonly IChannel9CategoryBrowser _categoryBrowser;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ShowSelectionVM"/> class.
        /// </summary>
        /// <param name="categoryBrowser">The browser that is used to retrieve shows.</param>
        [ImportingConstructor]
        public ShowSelectionVM(IChannel9CategoryBrowser categoryBrowser)
        {
            _categoryBrowser = categoryBrowser;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the collection of shows.
        /// </summary>
        public CollectionView ShowCollectionView
        {
            get; private set;
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Initializes this viewmodel.
        /// </summary>
        public void Initialize()
        {
            var shows = _categoryBrowser.GetAllShows();
            ShowCollectionView = (CollectionView)CollectionViewSource.GetDefaultView(shows);
        }

        #endregion Public Methods
    }
}