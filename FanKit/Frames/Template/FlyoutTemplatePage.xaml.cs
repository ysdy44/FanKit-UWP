using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Template
{
    public sealed partial class FlyoutTemplatePage : Page
    {
        public FlyoutTemplatePage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/FlyoutTemplatePage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/FlyoutTemplate.style.txt");
            };

            this.Button.Tapped += async (sender, e) =>
             {
                 this.Flyout.ShowAt(this.ButtonCenter);
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
             };
        }
    }
}
