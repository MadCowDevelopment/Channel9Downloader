using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Channel9Downloader.TemplateSelectors
{
    public class CategoryHeaderTemplateSelector : DataTemplateSelector
    {
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
    }
}
