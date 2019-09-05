using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    /// <summary>
    /// Page of RevealBorderBrush.
    /// </summary>
    public sealed partial class RevealBorderBrushPage : Page
    {
        //@Construct
        public RevealBorderBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemControlBackgroundAccentRevealBorderBrush";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}