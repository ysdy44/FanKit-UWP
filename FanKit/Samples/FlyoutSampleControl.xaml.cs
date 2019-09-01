using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FanKit.Samples
{
    public sealed partial class FlyoutSampleControl : UserControl
    {
        //@Content
        public void SetCoords(FrameworkElement element)
        {
            GeneralTransform transform = element.TransformToVisual(Window.Current.Content);

            Point screenCoords = transform.TransformPoint(new Point(0, 0));
            Point centerCoords = new Point(screenCoords.X + element.ActualWidth / 2, screenCoords.Y + element.ActualHeight / 2);

            Canvas.SetLeft(this, centerCoords.X - this.ActualWidth / 2);
            Canvas.SetTop(this, centerCoords.Y - this.ActualHeight / 2);
        }
        public void SetSample(Sample sample)
        {
            if (sample == null) return;

            Uri uri = sample.Uri;
            this.FlyoutImageEx.Source = uri;

            string name = sample.Name;
            this.FlyoutNameTextBlock.Text = name;

            string summary = sample.Summary;
            this.FlyoutSummaryTextBlock.Text = summary;
        }

        //@Construct
        public FlyoutSampleControl()
        {
            this.InitializeComponent();
        }
    }
}