using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Brush
{
    public sealed partial class SystemBrushPage : Page
    {
        public SystemBrushPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text ="<Border  Background=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemControlBackgroundAccentBrush";
            this.TopRun3.Text = "}\" /> ";
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
