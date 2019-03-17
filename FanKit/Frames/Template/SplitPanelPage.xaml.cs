using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FanKit.Frames.Template
{
    public sealed partial class SplitPanelPage : Page
    {
        public SplitPanelPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Template/SplitPanelPage.xaml.txt");
            this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Template/SplitPanelPage.xaml.cs.txt");
        }
               


        private double openOpacity;
        public double OpenOpacity
        {
            get => this.openOpacity;
            set
            {
                this.DismissOverlayBackground.Visibility = value == 0.0 ? Visibility.Collapsed : Visibility.Visible;
                this.DismissOverlayBackground.Opacity = value;
                this.SpliteLeftShadow.Opacity = value;
                this.BehaviorOffset.OffsetX = (value - 1) * this.ContentPresenter.ActualWidth;

                this.openOpacity = value;
            }
        }

        private double translateX = -300;
        public double TranslateX
        {
            set => translateX = value;
            get
            {
                if (translateX > 0.0d) return 0.0d;
                else if (translateX < -this.ContentPresenter.ActualWidth) return -this.ContentPresenter.ActualWidth;
                else return translateX;
            }
        }

        private new void ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
            this.BehaviorOffset.Duration = 0.0d;
            this.TranslateX = (this.OpenOpacity - 1) * this.ContentPresenter.ActualWidth; 
        }
        private new void ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.translateX += e.Delta.Translation.X;
            this.OpenOpacity = this.TranslateX / this.ContentPresenter.ActualWidth + 1;
        }
        private new void ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            this.BehaviorOffset.Duration = 600.0d;
            this.OpenOpacity = this.TranslateX > -this.ContentPresenter.ActualWidth / 2? 1.0: 0.0;
        }

        


        private void Panel_Tapped(object sender, TappedRoutedEventArgs e) => this.OpenOpacity = 0.0d;

        private void Button_Tapped(object sender, TappedRoutedEventArgs e) => this.OpenOpacity = this.OpenOpacity == 0.0d ? 1.0d : 0.0d;

    }
}





