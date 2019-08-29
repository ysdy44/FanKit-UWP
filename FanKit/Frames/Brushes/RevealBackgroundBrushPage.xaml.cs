using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    public sealed partial class RevealBackgroundBrushPage : Page
    {
        public RevealBackgroundBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemControlBackgroundAccentRevealBorderBrush";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}