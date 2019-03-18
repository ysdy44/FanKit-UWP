using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace FanKit.Frames.Template
{
    public sealed partial class FlyoutTemplatePage : Page
    {
        public FlyoutTemplatePage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Template/FlyoutTemplatePage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Template/FlyoutTemplate.style.txt");
            };
        }


        private async void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Flyout.ShowAt(this.Button);
            await Task.Delay(500);
            this.Flyout.Hide();

            this.FlyoutLeft.ShowAt(this.ButtonLeft);
            await Task.Delay(500);
            this.FlyoutLeft.Hide();

            this.FlyoutTop.ShowAt(this.ButtonTop);
            await Task.Delay(500);
            this.FlyoutTop.Hide();

            this.FlyoutRight.ShowAt(this.ButtonRight);
            await Task.Delay(500);
            this.FlyoutBottom.Hide();

            this.FlyoutBottom.ShowAt(this.ButtonBottom);
            await Task.Delay(500);
            this.FlyoutBottom.Hide();
        }
    }
}
