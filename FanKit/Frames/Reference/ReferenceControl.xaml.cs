using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace FanKit.Frames.Reference
{
    /// <summary>
    /// Control of reference.
    /// </summary>
    public partial class ReferenceControl : UserControl
    {
        //@Content
        /// <summary> Image's source. </summary>
        public ImageSource ImageSource { get=> this.Image.Source; set=> this.Image.Source=value; }
        /// <summary> Title's text. </summary>
        public string Title { get => this.TitleTextBlock.Text; set => this.TitleTextBlock.Text = value; }
        /// <summary> Summary's text. </summary>
        public string Summary { get => this.SummaryTextBlock.Text; set => this.SummaryTextBlock.Text = value; }

        /// <summary> Pasted text. </summary>
        public string PastedText
    {
            get => this.pastedText;
            set
            {
                this.PasteButton.Visibility = (value == null || value == string.Empty) ? Visibility.Collapsed : Visibility.Visible;
                this.pastedText = value;
            }
        }
        private string pastedText = null;
        /// <summary> Link's uri. </summary>
        public Uri LinkUri
        {
            get=>this.linkUri;
            set
            {
                this.LinkButton.Visibility = (value == null) ? Visibility.Collapsed : Visibility.Visible;
                this.linkUri = value;
            }
        }
        private Uri linkUri = null;
        

        //@Construct
        public ReferenceControl()
        {
            this.InitializeComponent();

            //Paste
            this.LinkButton.Tapped += async (s, e) =>
            {
                if (this.linkUri == null) return;

                Uri uri = this.linkUri;
                await Launcher.LaunchUriAsync(uri);
            };

            //Paste
            this.PasteButton.Tapped += async (s, e) =>
            {
                string text = this.PastedText;

                //Clipboard
                DataPackage dataPackage = new DataPackage();
                dataPackage.SetText(text);
                Clipboard.SetContent(dataPackage);

                //Tip
                this.TipBorder.Visibility = Visibility.Visible;
                await Task.Delay(1000);
                this.TipBorder.Visibility = Visibility.Collapsed;
            };
        }
    }
}