using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    /// <summary>
    /// Page of AcrylicElementBrush.
    /// </summary>
    public sealed partial class AcrylicElementBrushPage : Page
    {
        //@Construct
        public AcrylicElementBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemControlAcrylicElementBrush";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}