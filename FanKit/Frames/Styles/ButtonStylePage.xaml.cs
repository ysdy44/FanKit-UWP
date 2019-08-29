using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Styles
{
    public sealed partial class ButtonStylePage : Page
    {
        public ButtonStylePage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Button  Style=\"{ ThemeResource ";
            this.TopRun2.Text = "AccentButtonStyle";
            this.TopRun3.Text = "}\" /> ";
        }
    }
}