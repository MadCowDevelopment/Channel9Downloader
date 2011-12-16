using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Channel9Downloader.Controls
{
    /// <summary>
    /// Interaction logic for LoadingWait.xaml
    /// </summary>
    public partial class LoadingWait
    {
        #region Fields

        /// <summary>
        /// The timer used for the animation.
        /// </summary>
        private readonly DispatcherTimer _animationTimer;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingWait"/> class.
        /// </summary>
        public LoadingWait()
        {
            InitializeComponent();

            _animationTimer = new DispatcherTimer(
                DispatcherPriority.ContextIdle, Dispatcher);
            _animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 75);
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Rotate the spinner.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void HandleAnimationTick(object sender, EventArgs e)
        {
            SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
        }

        /// <summary>
        /// Handle loaded event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            const double Offset = Math.PI;
            const double Step = Math.PI * 2 / 10.0;

            SetPosition(C0, Offset, 0.0, Step);
            SetPosition(C1, Offset, 1.0, Step);
            SetPosition(C2, Offset, 2.0, Step);
            SetPosition(C3, Offset, 3.0, Step);
            SetPosition(C4, Offset, 4.0, Step);
            SetPosition(C5, Offset, 5.0, Step);
            SetPosition(C6, Offset, 6.0, Step);
            SetPosition(C7, Offset, 7.0, Step);
            SetPosition(C8, Offset, 8.0, Step);
        }

        /// <summary>
        /// Stop the animation.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            StopAnimation();
        }

        /// <summary>
        /// Handle the visible changed event.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void HandleVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var isVisible = (bool)e.NewValue;

            if (isVisible)
            {
                StartAnimation();
            }
            else
            {
                StopAnimation();
            }
        }

        /// <summary>
        /// Set position of the ellipse.
        /// </summary>
        /// <param name="ellipse">Ellipse whose position should be changed.</param>
        /// <param name="offset">General offset.</param>
        /// <param name="posOffSet">Offset for the position.</param>
        /// <param name="step">Step number.</param>
        private void SetPosition(Ellipse ellipse, double offset, double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, 50.0 + (Math.Sin(offset + (posOffSet * step)) * 50.0));
            ellipse.SetValue(Canvas.TopProperty, 50 + (Math.Cos(offset + (posOffSet * step)) * 50.0));
        }

        /// <summary>
        /// Start the animation.
        /// </summary>
        private void StartAnimation()
        {
            Mouse.OverrideCursor = Cursors.Wait;
            _animationTimer.Tick += HandleAnimationTick;
            _animationTimer.Start();
        }

        /// <summary>
        /// Stop the animation.
        /// </summary>
        private void StopAnimation()
        {
            _animationTimer.Stop();
            Mouse.OverrideCursor = Cursors.Arrow;
            _animationTimer.Tick -= HandleAnimationTick;
        }

        #endregion Private Methods
    }
}