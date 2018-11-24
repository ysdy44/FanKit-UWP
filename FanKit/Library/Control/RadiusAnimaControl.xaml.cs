using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Library.Control
{
    public sealed partial class RadiusAnimaPanel : UserControl
    {
        public UIElement CenterContent { get => this.Border.Child; set => this.Border.Child = value; }

        public RadiusAnimaPanel()
        {
            this.InitializeComponent();
        }

        private void Border_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Frame.Value = e.NewSize.Width;
            this.Storyboard.Begin();
        }
    }
}
