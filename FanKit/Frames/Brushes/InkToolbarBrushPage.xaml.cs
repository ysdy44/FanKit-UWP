using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    public sealed partial class InkToolbarBrushPage : Page
    {
        public InkToolbarBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "InkToolbarButtonBackgroundThemeBrush";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}