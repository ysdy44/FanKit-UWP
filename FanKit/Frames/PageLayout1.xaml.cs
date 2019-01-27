using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames
{
    public sealed partial class PageLayout1 : UserControl
    {
        //Layout
        public UIElement ShowContent { get => this.ShowBorder.Child; set => this.ShowBorder.Child = value; }
        public UIElement DetailContent { get => this.DetailBorder.Child; set => this.DetailBorder.Child = value; }

        public PageLayout1()
        {
            this.InitializeComponent();
        }
    }
}
