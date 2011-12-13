namespace Channel9Downloader.Entities
{
    public class Show : RecurringCategory
    {
        public Show(RecurringCategory recurringCategory)
        {
            Description = recurringCategory.Description;
            RelativePath = recurringCategory.RelativePath;
            Title = recurringCategory.Title;
        }
    }
}
