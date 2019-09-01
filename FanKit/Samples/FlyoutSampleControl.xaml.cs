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

            double x = centerCoords.X - this.ActualWidth / 2;
            double y = centerCoords.Y - this.ActualHeight / 2;

            if (x < 0) x = 0;
            if (y < 0) y = 0;
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }
        public void SetSample(Sample sample)
        {
            if (sample == null) return;

            Uri uri = sample.Uri;
            this.Image.Source = uri;

            string name = sample.Name;
            this.NameTextbolck.Text = name;

            string summary = sample.Summary;
            this.SummaryContentControl.Content = new TextBlock
            {
                Text = summary,
                TextWrapping = TextWrapping.WrapWholeWords
            };
        }

        //@Construct
        public FlyoutSampleControl()
        {
            this.InitializeComponent();
        }
    }
}