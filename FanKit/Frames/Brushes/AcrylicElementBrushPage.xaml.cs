using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    public sealed partial class AcrylicElementBrushPage : Page
    {
        public AcrylicElementBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemControlAcrylicElementBrush";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}