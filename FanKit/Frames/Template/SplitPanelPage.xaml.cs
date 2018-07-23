using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/SplitPanelXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/SplitPanelCs.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/SplitPanelUserCs.txt");
            this.MarkdownText4.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/SplitPanelUserCs.txt");
        }


        #region Splite：侧栏


        private bool isPaneOpen=false;
        public bool IsPaneOpen
        {
            set
            {
                if (value == true)
                {
                    this.OpenPanel.Visibility = Visibility.Visible;
                    this.SpliteShadow.Opacity = 1.0d;

                    this.SpliteShow.Begin();

                    this.TranslateX = this.Transform.TranslateX = this.RightDistance;
                }
                else
                {
                    this.SpliteFade.Begin();

                    this.SpliteShadow.Opacity = 0.0d;
                    this.OpenPanel.Visibility = Visibility.Collapsed;

                    this.TranslateX = this.Transform.TranslateX = this.LeftDistance;
                }
                this.isPaneOpen = value;
            }
            get=> this.isPaneOpen;
         }

        private double translateX= -300;
        public double TranslateX
        {
            set
            {
                if (value > this.RightDistance) translateX = this.RightDistance;
                else if (value < this.LeftDistance) translateX = this.LeftDistance;
                else translateX = value;
            }
            get => translateX;
        }

        public CompositeTransform Transform
        {
            set=>   this.OpenGrid.RenderTransform = value;
            get
            {
                if (this.OpenGrid.RenderTransform is CompositeTransform transform) return transform;
                else return null;
            }
        }


        private readonly double LeftDistance = -300;
        private readonly double Boundary = -125;
        private readonly double RightDistance = 0;


        private new void ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {
             this.OpenPanel.Visibility = Visibility.Visible;
            this.SpliteShadow.Opacity = 1.0d;
        }
        private new void ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            this.TranslateX += e.Delta.Translation.X;
            this.Transform.TranslateX = this.TranslateX;
            this.OpenPanel.Opacity = (this.TranslateX - this.LeftDistance) / (this.RightDistance - this.LeftDistance);
        }
        private new void ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            this.IsPaneOpen = this.Transform.TranslateX >= this.Boundary;
        }


        #endregion


        private void Panel_Tapped(object sender, TappedRoutedEventArgs e)=>   this.IsPaneOpen = false; 

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)=>this.IsPaneOpen = !this.IsPaneOpen;

    }
}
