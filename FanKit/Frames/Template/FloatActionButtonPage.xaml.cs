using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Template
{
    public sealed partial class FloatActionButtonPage : Page
    {
        public FloatActionButtonPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/FloatActionButtonXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/FloatActionButtonStyle.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/FloatActionButtonCs.txt");
        }




        //Property

        private double Span = 0;//cache

        private bool isShow; //main
        public bool IsShow
        {
            get => isShow;
            set
            {
                if (value != isShow)
                {
                    if (value) this.Button.Visibility = Visibility.Collapsed;
                    else this.Button.Visibility = Visibility.Visible;

                    isShow = value;
                }
            }
        }

        private double verticalOffset;//offset
        public double VerticalOffset
        {
            get => verticalOffset;
            set
            {
                if (this.IsShow == false && value > verticalOffset)
                    Span += verticalOffset - value; //Down: cache offset
                else if (this.IsShow == true && value < verticalOffset) 
                    Span += verticalOffset - value;//Up: cache offset

                //Up: overflow
                if (Span > 20) this.IsShow = false;
                //Down: overflow
                else if (Span < -20) this.IsShow = true;

                verticalOffset = value;
            }
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e) => this.VerticalOffset = ((ScrollViewer)sender).VerticalOffset;





        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.Button.Visibility == Visibility.Visible)
                this.Button.Visibility = Visibility.Collapsed;
            else
                this.Button.Visibility = Visibility.Visible;
        }


    }
}
