using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Channel9Downloader.Controls
{
    /// <summary>
    /// This class can be used as container for adorned content.
    /// </summary>
    public class AdornedControl : ContentControl
    {
        #region Fields

        /// <summary>
        /// Register <see cref="AdornerContent"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AdornerContentProperty = DependencyProperty.Register(
            "AdornerContent",
            typeof(object),
            typeof(AdornedControl),
            new FrameworkPropertyMetadata(AdornerContentPropertyChanged));

        /// <summary>
        /// Register <see cref="AdornerOffsetX"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AdornerOffsetXProperty = DependencyProperty.Register(
            "AdornerOffsetX", typeof(double), typeof(AdornedControl));

        /// <summary>
        /// Register <see cref="AdornerOffsetY"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty AdornerOffsetYProperty = DependencyProperty.Register(
            "AdornerOffsetY", typeof(double), typeof(AdornedControl));

        /// <summary>
        /// Command to hide the Adorner.
        /// </summary>
        public static readonly RoutedCommand HideAdornerCommand = new RoutedCommand(
            "HideAdorner", typeof(AdornedControl));

        /// <summary>
        /// Register <see cref="HorizontalAdornerPlacement"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty HorizontalAdornerPlacementProperty =
            DependencyProperty.Register(
                "HorizontalAdornerPlacement",
                typeof(AdornerPlacement),
                typeof(AdornedControl),
                new FrameworkPropertyMetadata(AdornerPlacement.Inside));

        /// <summary>
        /// Register <see cref="IsAdornerVisible"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsAdornerVisibleProperty =
            DependencyProperty.Register(
                "IsAdornerVisible",
                typeof(bool),
                typeof(AdornedControl),
                new FrameworkPropertyMetadata(IsAdornerVisiblePropertyChanged));

        /// <summary>
        /// Command to show the Adorner.
        /// </summary>
        public static readonly RoutedCommand ShowAdornerCommand = new RoutedCommand("ShowAdorner", typeof(AdornedControl));

        /// <summary>
        /// Register <see cref="VerticalAdornerPlacement"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty VerticalAdornerPlacementProperty =
            DependencyProperty.Register(
                "VerticalAdornerPlacement",
                typeof(AdornerPlacement),
                typeof(AdornedControl),
                new FrameworkPropertyMetadata(AdornerPlacement.Inside));

        /// <summary>
        /// Command to hide the adorner.
        /// </summary>
        private static readonly CommandBinding HideAdornerCommandBinding = new CommandBinding(HideAdornerCommand, HideAdornerCommandExecuted);

        /// <summary>
        /// Command to show the adorner.
        /// </summary>
        private static readonly CommandBinding ShowAdornerCommandBinding = new CommandBinding(ShowAdornerCommand, ShowAdornerCommandExecuted);

        /// <summary>
        /// The adorned framework element.
        /// </summary>
        private FrameworkElement _adornedFrameworkElement;

        /// <summary>
        /// The actual adorner create to contain our 'adorner UI content'.
        /// </summary>
        private FrameworkElementAdorner _adorner;

        /// <summary>
        /// Caches the adorner layer.
        /// </summary>
        private AdornerLayer _adornerLayer;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the AdornedControl class.
        /// </summary>
        static AdornedControl()
        {
            CommandManager.RegisterClassCommandBinding(typeof(AdornedControl), ShowAdornerCommandBinding);
            CommandManager.RegisterClassCommandBinding(typeof(AdornedControl), HideAdornerCommandBinding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdornedControl"/> class.
        /// </summary>
        public AdornedControl()
        {
            DataContextChanged += AdornedControlDataContextChanged;
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets the adorner content.
        /// </summary>
        public object AdornerContent
        {
            get
            {
                return GetValue(AdornerContentProperty);
            }

            set
            {
                SetValue(AdornerContentProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the X offset of the adorner.
        /// </summary>
        public double AdornerOffsetX
        {
            get
            {
                return (double)GetValue(AdornerOffsetXProperty);
            }

            set
            {
                SetValue(AdornerOffsetXProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the Y offset of the adorner.
        /// </summary>
        public double AdornerOffsetY
        {
            get
            {
                return (double)GetValue(AdornerOffsetYProperty);
            }

            set
            {
                SetValue(AdornerOffsetYProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the horizontal placement of the adorner relative to the adorned control.
        /// </summary>
        public AdornerPlacement HorizontalAdornerPlacement
        {
            get
            {
                return (AdornerPlacement)GetValue(HorizontalAdornerPlacementProperty);
            }

            set
            {
                SetValue(HorizontalAdornerPlacementProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the adorner is visible.
        /// </summary>
        public bool IsAdornerVisible
        {
            get
            {
                return (bool)GetValue(IsAdornerVisibleProperty);
            }

            set
            {
                SetValue(IsAdornerVisibleProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the vertical placement of the adorner relative to the adorned control.
        /// </summary>
        public AdornerPlacement VerticalAdornerPlacement
        {
            get
            {
                return (AdornerPlacement)GetValue(VerticalAdornerPlacementProperty);
            }

            set
            {
                SetValue(VerticalAdornerPlacementProperty, value);
            }
        }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// Hide the adorner.
        /// </summary>
        public void HideAdorner()
        {
            IsAdornerVisible = false;
        }

        /// <summary>
        /// Applies the template.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ShowOrHideAdornerInternal();
        }

        /// <summary>
        /// Show the adorner.
        /// </summary>
        public void ShowAdorner()
        {
            IsAdornerVisible = true;
        }

        #endregion Public Methods

        #region Private Static Methods

        /// <summary>
        /// Event raised when the value of <see cref="AdornerContent"/> has changed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private static void AdornerContentPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var c = (AdornedControl)sender;
            c.ShowOrHideAdornerInternal();
        }

        /// <summary>
        /// Event raised when the Hide command is executed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private static void HideAdornerCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var c = (AdornedControl)sender;
            c.HideAdorner();
        }

        /// <summary>
        /// Event raised when the value of <see cref="IsAdornerVisible"/> has changed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private static void IsAdornerVisiblePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var c = (AdornedControl)sender;
            c.ShowOrHideAdornerInternal();
        }

        /// <summary>
        /// Event raised when the Show command is executed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private static void ShowAdornerCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var c = (AdornedControl)sender;
            c.ShowAdorner();
        }

        #endregion Private Static Methods

        #region Private Methods

        /// <summary>
        /// Event raised when the DataContext of the adorned control changes.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void AdornedControlDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateAdornerDataContext();
        }

        /// <summary>
        /// Creates the adorned framework element by searching for a <see cref="DataTemplate"/> that has the same type
        /// as the <see cref="AdornerContent"/>.
        /// </summary>
        private void CreateAdornedFrameworkElementFromDataTemplateInResources()
        {
            foreach (var resourceDictionary in Application.Current.Resources.MergedDictionaries)
            {
                foreach (var resource in resourceDictionary.Values.OfType<DataTemplate>())
                {
                    if ((Type)resource.DataType == AdornerContent.GetType())
                    {
                        _adornedFrameworkElement = resource.LoadContent() as FrameworkElement;
                        if (_adornedFrameworkElement != null)
                        {
                            _adornedFrameworkElement.DataContext = AdornerContent;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Internal method to hide the adorner.
        /// </summary>
        private void HideAdornerInternal()
        {
            if (_adornerLayer == null || _adorner == null)
            {
                // Not already adorned.
                return;
            }

            _adornerLayer.Remove(_adorner);
            _adorner.DisconnectChild();

            _adorner = null;
            _adornerLayer = null;
        }

        /// <summary>
        /// Internal method to show the adorner.
        /// </summary>
        private void ShowAdornerInternal()
        {
            if (_adorner != null)
            {
                // Already adorned.
                return;
            }

            if (AdornerContent != null)
            {
                if (_adornerLayer == null)
                {
                    _adornerLayer = AdornerLayer.GetAdornerLayer(this);
                }

                if (AdornerContent is FrameworkElement)
                {
                    _adornedFrameworkElement = AdornerContent as FrameworkElement;
                }
                else
                {
                    CreateAdornedFrameworkElementFromDataTemplateInResources();
                }

                if (_adornerLayer != null && _adornedFrameworkElement != null)
                {
                    _adorner = new FrameworkElementAdorner(
                        _adornedFrameworkElement,
                        this,
                        HorizontalAdornerPlacement,
                        VerticalAdornerPlacement,
                        AdornerOffsetX,
                        AdornerOffsetY);
                    _adornerLayer.Add(_adorner);

                    UpdateAdornerDataContext();
                }
            }
        }

        /// <summary>
        /// Internal method to show or hide the adorner based on the value of <see cref="IsAdornerVisible"/>.
        /// </summary>
        private void ShowOrHideAdornerInternal()
        {
            if (IsAdornerVisible)
            {
                ShowAdornerInternal();
            }
            else
            {
                HideAdornerInternal();
            }
        }

        /// <summary>
        /// Update the DataContext of the adorner from the adorned control.
        /// </summary>
        private void UpdateAdornerDataContext()
        {
            if (_adornedFrameworkElement != null)
            {
                if (_adornedFrameworkElement.DataContext == null)
                {
                    _adornedFrameworkElement.DataContext = DataContext;
                }
            }
        }

        #endregion Private Methods
    }
}