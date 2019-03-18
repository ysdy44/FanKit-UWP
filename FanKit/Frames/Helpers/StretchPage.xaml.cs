using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Helpers
{
    public sealed partial class StretchPage : Page
    {
        public StretchPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Image  Stretch=\"";
            this.TopRun2.Text = "None";
            this.TopRun3.Text = "\" /> ";
         }        
    }
}
