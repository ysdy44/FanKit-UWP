using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Template
{
    public sealed partial class FlyoutTemplatePage : Page
    {
        //@Construct
        public FlyoutTemplatePage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/FlyoutTemplatePage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Template/FlyoutTemplatePage.xaml"));
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
