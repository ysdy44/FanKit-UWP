using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Template
{
    public sealed partial class TagChipPage : Page
    {
        //@Construct
        public TagChipPage()
        {
            this.InitializeComponent();
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/TagChipPage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Template/TagChipPage.xaml"));
            };
        }
        

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is string s)
            {
                this.TexBlock.Text = s;
                this.TexBlockBorder.Visibility = Visibility.Visible;

                await Task.Delay(1500);
                this.TexBlockBorder.Visibility = Visibility.Collapsed;
            } 
        }

    } 
}
