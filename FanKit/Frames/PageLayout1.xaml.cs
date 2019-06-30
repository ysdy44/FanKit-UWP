using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames
{
    /// <summary>
    /// Layer of <see cref="Page">.
    /// </summary>
    public sealed partial class PageLayout1 : UserControl
    {
        //@Content
        /// <summary> Showcase area </summary>
        public UIElement ShowContent { get => this.ShowBorder.Child; set => this.ShowBorder.Child = value; }
        /// <summary> Detailed area </summary>
        public UIElement DetailContent { get => this.DetailBorder.Child; set => this.DetailBorder.Child = value; }

        //@Construct
        public PageLayout1()
        {
            this.InitializeComponent();
        }
    }
}