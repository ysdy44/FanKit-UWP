using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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
