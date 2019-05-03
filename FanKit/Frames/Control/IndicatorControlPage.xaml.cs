using FanKit.Control;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Control
{
    public sealed partial class IndicatorControlPage : Page
    {
        public IndicatorControlPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/IndicatorControlPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/IndicatorControl.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Control/IndicatorControl.cs.txt");
            };

            //IndicatorControl
            this.Slider.ValueChanged += (s, e) => this.IndicatorControl.Radians = e.NewValue;
            this.IndicatorControl.ModeChanged += (mode) => this.TextBlock.Text = mode.ToString();

            //Button
            this.LeftTopButton.Tapped += (s, e) => this.IndicatorControl.Mode = IndicatorMode.LeftTop;
            this.RightTopButton.Tapped += (s, e) => this.IndicatorControl.Mode = IndicatorMode.RightTop;
            this.RightBottomButton.Tapped += (s, e) => this.IndicatorControl.Mode = IndicatorMode.RightBottom;
            this.LeftBottomButton.Tapped += (s, e) => this.IndicatorControl.Mode = IndicatorMode.LeftBottom;

            this.LeftButton.Tapped += (s, e) => this.IndicatorControl.Mode = IndicatorMode.Left;
            this.TopButton.Tapped += (s, e) => this.IndicatorControl.Mode = IndicatorMode.Top;
            this.RightButton.Tapped += (s, e) => this.IndicatorControl.Mode = IndicatorMode.Right;
            this.BottomButton.Tapped += (s, e) => this.IndicatorControl.Mode = IndicatorMode.Bottom;

            this.CenterButton.Tapped += (s, e) => this.IndicatorControl.Mode = IndicatorMode.Center;
        }
    }
}
