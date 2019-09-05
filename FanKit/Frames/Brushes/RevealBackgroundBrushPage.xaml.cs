using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    /// <summary>
    /// Page of RevealBackgroundBrush.
    /// </summary>
    public sealed partial class RevealBackgroundBrushPage : Page
    {
        //@Construct
        public RevealBackgroundBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemControlBackgroundAccentRevealBorderBrush";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}