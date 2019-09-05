using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    /// <summary>
    /// Page of InkToolbarBrush.
    /// </summary>
    public sealed partial class InkToolbarBrushPage : Page
    {
        //@Construct
        public InkToolbarBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "InkToolbarButtonBackgroundThemeBrush";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}