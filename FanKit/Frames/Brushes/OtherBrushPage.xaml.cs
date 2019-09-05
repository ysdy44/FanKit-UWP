using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Brushes
{
    /// <summary>
    /// Page of OtherBrush.
    /// </summary>
    public sealed partial class OtherBrushPage : Page
    {
        //@Construct
        public OtherBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "ScrollBarTrackFill";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}