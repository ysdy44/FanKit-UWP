using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FanKit.Samples
{
    /// <summary>
    /// A Billboard to show the a image and propertys.
    /// </summary>
    public sealed partial class Billboard : UserControl
    {
        double actualWidth = 200;
        double actualHeight = 300;

        #region DependencyProperty

        /// <summary> Gets or sets the Sample. </summary>
        public Sample Sample
        {
            get { return (Sample)GetValue(SampleProperty); }
            set { SetValue(SampleProperty, value); }
        }
        /// <summary> Identifies the <see cref = "Billboard.Sample" /> dependency property. </summary>
        public static readonly DependencyProperty SampleProperty = DependencyProperty.Register(nameof(Sample), typeof(Sample), typeof(Billboard), new PropertyMetadata(new Sample()));

        #endregion


        //@Construct
        /// <summary>
        /// Initializes a Billboard. 
        /// </summary>
        public Billboard()
        {
            this.InitializeComponent();
            this.SizeChanged += (s, e) =>
            {
                if (e.NewSize == e.PreviousSize) return;
                this.actualWidth = e.NewSize.Width;
                this.actualHeight = e.NewSize.Height;
            };
        }

        public void CalculatePostion(FrameworkElement placementTarget)
        {
            GeneralTransform transform = placementTarget.TransformToVisual(Window.Current.Content);

            Point screenCoords = transform.TransformPoint(new Point(0, 0));
            double centerCoordsX = screenCoords.X + placementTarget.ActualWidth / 2;
            double centerCoordsY = screenCoords.Y + placementTarget.ActualHeight / 2;

            double x = centerCoordsX - this.actualWidth / 2;
            double y = centerCoordsY - this.actualHeight / 2;

            if (x < 0) x = 0;
            if (y < 0) y = 0;
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

    }
}