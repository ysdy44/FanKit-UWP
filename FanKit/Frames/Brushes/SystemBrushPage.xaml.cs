using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    /// <summary>
    /// Page of SystemBrush.
    /// </summary>
    public sealed partial class SystemBrushPage : Page
    {
        //@Construct
        public SystemBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text ="<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemControlBackgroundAccentBrush";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}