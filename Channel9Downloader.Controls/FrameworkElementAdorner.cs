using System.Collections;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Channel9Downloader.Controls
{
    /// <summary>
    /// This class is an adorner that allows a <see cref="FrameworkElement"/> derived class 
    /// to adorn another <see cref="FrameworkElement"/>.
    /// </summary>
    public class FrameworkElementAdorner : Adorner
    {
        #region Fields

        /// <summary>
        /// The framework element that is the adorner.
        /// </summary>
        private readonly FrameworkElement _child;

        /// <summary>
        /// Horizontal placement of the child.
        /// </summary>
        private readonly AdornerPlacement _horizontalAdornerPlacement = AdornerPlacement.Inside;

        /// <summary>
        /// X offset of the child.
        /// </summary>
        private readonly double _offsetX;

        /// <summary>
        /// Y offset of the child.
        /// </summary>
        private readonly double _offsetY;

        /// <summary>
        /// Vertical placement of the child.
        /// </summary>
        private readonly AdornerPlacement _verticalAdornerPlacement = AdornerPlacement.Inside;

        /// <summary>
        /// X position of the child (when not set to NaN).
        /// </summary>
        private double _positionX = double.NaN;

        /// <summary>
        /// Y position of the child (when not set to NaN).
        /// </summary>
        private double _positionY = double.NaN;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FrameworkElementAdorner class.
        /// </summary>
        /// <param name="adornerChildElement">The adorner child element.</param>
        /// <param name="adornedElement">The adorned element.</param>
        public FrameworkElementAdorner(FrameworkElement adornerChildElement, FrameworkElement adornedElement)
            : base(adornedElement)
        {
            _child = adornerChildElement;

            AddLogicalChild(adornerChildElement);
            AddVisualChild(adornerChildElement);
        }

        /// <summary>
        /// Initializes a new instance of the FrameworkElementAdorner class.
        /// </summary>
        /// <param name="adornerChildElement">The adorner child element.</param>
        /// <param name="adornedElement">The adorned element.</param>
        /// <param name="horizontalAdornerPlacement">Horizontal placement of the adorner.</param>
        /// <param name="verticalAdornerPlacement">Vertical placement of the adorner.</param>
        /// <param name="offsetX">X offset of the adorner.</param>
        /// <param name="offsetY">Y offset of the adorner.</param>
        public FrameworkElementAdorner(
            FrameworkElement adornerChildElement,
            FrameworkElement adornedElement,
            AdornerPlacement horizontalAdornerPlacement,
            AdornerPlacement verticalAdornerPlacement,
            double offsetX,
            double offsetY)
            : base(adornedElement)
        {
            _child = adornerChildElement;
            _horizontalAdornerPlacement = horizontalAdornerPlacement;
            _verticalAdornerPlacement = verticalAdornerPlacement;
            _offsetX = offsetX;
            _offsetY = offsetY;

            adornedElement.SizeChanged += AdornedElementSizeChanged;

            AddLogicalChild(adornerChildElement);
            AddVisualChild(adornerChildElement);
        }

        #endregion Constructors

        #region Public Properties

        /// <summary>
        /// Gets the adorned element.
        /// </summary>
        public new FrameworkElement AdornedElement
        {
            get
            {
                return (FrameworkElement)base.AdornedElement;
            }
        }

        /// <summary>
        /// Gets or sets the X position of the child (when not set to NaN).
        /// </summary>
        public double PositionX
        {
            get
            {
                return _positionX;
            }

            set
            {
                _positionX = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y position of the child (when not set to NaN).
        /// </summary>
        public double PositionY
        {
            get
            {
                return _positionY;
            }

            set
            {
                _positionY = value;
            }
        }

        #endregion Public Properties

        #region Protected Properties

        /// <summary>
        /// Gets the logical children.
        /// </summary>
        protected override IEnumerator LogicalChildren
        {
            get
            {
                var list = new ArrayList { _child };
                return list.GetEnumerator();
            }
        }

        /// <summary>
        /// Gets the visual children count.
        /// </summary>
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }

        #endregion Protected Properties

        #region Public Methods

        /// <summary>
        /// Disconnect the child element from the visual tree so that it may be reused later.
        /// </summary>
        public void DisconnectChild()
        {
            RemoveLogicalChild(_child);
            RemoveVisualChild(_child);
        }

        #endregion Public Methods

        #region Protected Methods

        /// <summary>
        /// Positions child elements and determines a size for the <see cref="FrameworkElement"/> derived class. 
        /// </summary>
        /// <param name="finalSize">The final area within the parent that the element should use to 
        /// arrange itself and its children.</param>
        /// <returns>Returns the determined size.</returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            double x = PositionX;
            if (double.IsNaN(x))
            {
                x = DetermineX();
            }

            double y = PositionY;
            if (double.IsNaN(y))
            {
                y = DetermineY();
            }

            double adornerWidth = DetermineWidth();
            double adornerHeight = DetermineHeight();
            _child.Arrange(new Rect(x, y, adornerWidth, adornerHeight));
            return finalSize;
        }

        /// <summary>
        /// Gets the visual child with the specified index.
        /// </summary>
        /// <param name="index">Index of the child.</param>
        /// <returns>Returns the child.</returns>
        protected override Visual GetVisualChild(int index)
        {
            return _child;
        }

        /// <summary>
        /// Implements the custom measuring behavior for the adorner.
        /// </summary>
        /// <param name="constraint">The size constraint.</param>
        /// <returns>Returns the desired sized.</returns>
        protected override Size MeasureOverride(Size constraint)
        {
            _child.Measure(constraint);
            return _child.DesiredSize;
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Event raised when the adorned control's size has changed.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event args of the event.</param>
        private void AdornedElementSizeChanged(object sender, SizeChangedEventArgs e)
        {
            InvalidateMeasure();
        }

        /// <summary>
        /// Determine the height of the child.
        /// </summary>
        /// <returns>Returns the determined height.
        /// </returns>
        private double DetermineHeight()
        {
            if (!double.IsNaN(PositionY))
            {
                return _child.DesiredSize.Height;
            }

            switch (_child.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    {
                        return _child.DesiredSize.Height;
                    }

                case VerticalAlignment.Bottom:
                    {
                        return _child.DesiredSize.Height;
                    }

                case VerticalAlignment.Center:
                    {
                        return _child.DesiredSize.Height;
                    }

                case VerticalAlignment.Stretch:
                    {
                        return AdornedElement.ActualHeight;
                    }
            }

            return 0.0;
        }

        /// <summary>
        /// Determine the width of the child.
        /// </summary>
        /// <returns>Returns the determined width.</returns>
        private double DetermineWidth()
        {
            if (!double.IsNaN(PositionX))
            {
                return _child.DesiredSize.Width;
            }

            switch (_child.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        return _child.DesiredSize.Width;
                    }

                case HorizontalAlignment.Right:
                    {
                        return _child.DesiredSize.Width;
                    }

                case HorizontalAlignment.Center:
                    {
                        return _child.DesiredSize.Width;
                    }

                case HorizontalAlignment.Stretch:
                    {
                        return AdornedElement.ActualWidth;
                    }
            }

            return 0.0;
        }

        /// <summary>
        /// Determines the X coordinate of the child.
        /// </summary>
        /// <returns>Returns the determined X value.</returns>
        private double DetermineX()
        {
            switch (_child.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    {
                        if (_horizontalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            return -_child.DesiredSize.Width + _offsetX;
                        }

                        return _offsetX;
                    }

                case HorizontalAlignment.Right:
                    {
                        if (_horizontalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            double adornedWidth = AdornedElement.ActualWidth;
                            return adornedWidth + _offsetX;
                        }
                        else
                        {
                            double adornerWidth = _child.DesiredSize.Width;
                            double adornedWidth = AdornedElement.ActualWidth;
                            double x = adornedWidth - adornerWidth;
                            return x + _offsetX;
                        }
                    }

                case HorizontalAlignment.Center:
                    {
                        double adornerWidth = _child.DesiredSize.Width;
                        double adornedWidth = AdornedElement.ActualWidth;
                        double x = (adornedWidth / 2) - (adornerWidth / 2);
                        return x + _offsetX;
                    }

                case HorizontalAlignment.Stretch:
                    {
                        return 0.0;
                    }
            }

            return 0.0;
        }

        /// <summary>
        /// Determine the Y coordinate of the child.
        /// </summary>
        /// <returns>Returns the determined Y value.</returns>
        private double DetermineY()
        {
            switch (_child.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    {
                        if (_verticalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            return -_child.DesiredSize.Height + _offsetY;
                        }

                        return _offsetY;
                    }

                case VerticalAlignment.Bottom:
                    {
                        if (_verticalAdornerPlacement == AdornerPlacement.Outside)
                        {
                            double adornedHeight = AdornedElement.ActualHeight;
                            return adornedHeight + _offsetY;
                        }
                        else
                        {
                            double adornerHeight = _child.DesiredSize.Height;
                            double adornedHeight = AdornedElement.ActualHeight;
                            double x = adornedHeight - adornerHeight;
                            return x + _offsetY;
                        }
                    }

                case VerticalAlignment.Center:
                    {
                        double adornerHeight = _child.DesiredSize.Height;
                        double adornedHeight = AdornedElement.ActualHeight;
                        double x = (adornedHeight / 2) - (adornerHeight / 2);
                        return x + _offsetY;
                    }

                case VerticalAlignment.Stretch:
                    {
                        return 0.0;
                    }
            }

            return 0.0;
        }

        #endregion Private Methods
    }
}