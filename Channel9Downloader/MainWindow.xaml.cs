﻿using System;

using Channel9Downloader.Composition;
using Channel9Downloader.ViewModels;

namespace Channel9Downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                var composer = new DependencyComposer();
                composer.ComposeExportedValue<IDependencyComposer>(composer);

                DataContext = composer.GetExportedValue<IMainWindowVM>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
