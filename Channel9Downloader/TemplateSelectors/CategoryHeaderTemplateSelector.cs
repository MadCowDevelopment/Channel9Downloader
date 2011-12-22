using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Channel9Downloader.TemplateSelectors
{
    /// <summary>
    /// Template selector for category headers.
    /// </summary>
    public class CategoryHeaderTemplateSelector : DataTemplateSelector
    {
        #region Public Methods

        /// <summary>
        /// Selects the correct template for category selection group headers.
        /// </summary>
        /// <param name="item">The item for which a template should be applied.</param>
        /// <param name="container">The container of the item.</param>
        /// <returns>Returns the correct data template.</returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;

            if (element != null && item != null && item is CollectionViewGroup)
            {
                var collectionViewGroup = item as CollectionViewGroup;

                if (collectionViewGroup.Name.Equals("Enabled"))
                {
                    return element.FindResource("EnabledGroupTemplate") as DataTemplate;
                }

                if (collectionViewGroup.Name.Equals("Disabled"))
                {
                    return element.FindResource("DisabledGroupTemplate") as DataTemplate;
                }

                return element.FindResource("AlphaticalGroupTemplate") as DataTemplate;
            }

            return null;
        }

        #endregion Public Methods
    }
}