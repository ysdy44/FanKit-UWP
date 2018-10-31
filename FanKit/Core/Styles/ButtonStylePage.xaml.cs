using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            double Offset = ((ScrollViewer)sender).VerticalOffset;

            //ImageButtonVisible：图片按钮可视
            FanKit.MainPage.ImageButtonVisibleChange(Offset);

            //顶栏
            this.TopGrid.Height = App.GetHeight(Offset);
        }

    }
}
