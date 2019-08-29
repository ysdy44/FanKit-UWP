using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    public sealed partial class AcrylicWindowBrushPage : Page
    {
        public AcrylicWindowBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemControlAcrylicWindowBrush";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}