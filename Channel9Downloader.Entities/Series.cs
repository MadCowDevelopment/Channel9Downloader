namespace Channel9Downloader.Entities
{
    public class Series : RecurringCategory
    {
        public Series(RecurringCategory recurringCategory)
        {
            Description = recurringCategory.Description;
            RelativePath = recurringCategory.RelativePath;
            Title = recurringCategory.Title;
        }
    }
}
