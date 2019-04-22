using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Template
{
    public sealed partial class TagChipPage : Page
    {
        public TagChipPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/TagChipPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Template/TagChip.style.txt");
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
