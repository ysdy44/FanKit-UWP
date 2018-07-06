using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Brush
{
    public sealed partial class ColorPage : Page
    {
        public ColorPage()
        {
            this.InitializeComponent();

            this.Paragraph1.Text = "<Border>";
            this.Paragraph2.Text = "  <Border.Background>";

            this.TopRun1.Text = "    <SolidColorBrush  Color=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemAltHighColor";
            this.TopRun3.Text = "}\" /> ";

            this.Paragraph4.Text = "  </Border.Background>";
            this.Paragraph5.Text = "</Border>";
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e) => SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = this.Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;


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
