using FanKit.Transformers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Transformers
{
    /// <summary>
    /// Page of <see cref="FanKit.Transformers.IndicatorControl">.
    /// </summary>
    public sealed partial class IndicatorControlPage : Page
    {

        #region DependencyProperty
        
        /// <summary> CanvasTransformer's radian. </summary>
        public double Radian
        {
            get { return (double)GetValue(RadianProperty); }
            set { SetValue(RadianProperty, value); }
        }
        /// <summary> Identifies the <see cref = "IndicatorControlPage.Radian" /> dependency property. </summary>
        public static readonly DependencyProperty RadianProperty = DependencyProperty.Register(nameof(Radian), typeof(double), typeof(IndicatorControlPage), new PropertyMetadata(0.0d, (sender, e) =>
        {
            IndicatorControlPage con = (IndicatorControlPage)sender;

            if (e.NewValue is double value)
            {
                con.IndicatorControl.Radians = value;
            }
        }));
        
        #endregion

        //@Construct
        public IndicatorControlPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/IndicatorControlPage.xaml.txt");
            };

            //IndicatorControl
            this.IndicatorControl.ModeChanged += (mode) => this.ModeRun.Text = mode.ToString();

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