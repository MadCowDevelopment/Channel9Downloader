﻿namespace Channel9Downloader.Entities
{
    /// <summary>
    /// This class serves as base class for different video categories.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the title of the category.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the relative path for the category.
        /// </summary>
        public string RelativePath { get; set; }
    }
}