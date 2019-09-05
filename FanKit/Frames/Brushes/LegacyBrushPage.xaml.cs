using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    /// <summary>
    /// Page of LegacyBrush.
    /// </summary>
    public sealed partial class LegacyBrushPage : Page
    {
        //@Construct
        public LegacyBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "AppBarBackgroundThemeBrush";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}