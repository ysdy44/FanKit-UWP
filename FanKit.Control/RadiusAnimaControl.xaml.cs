using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Control
{
    public sealed partial class RadiusAnimaPanel : UserControl
    {
        public UIElement CenterContent { get => this.Border.Child; set => this.Border.Child = value; }

        public RadiusAnimaPanel()
        {
            this.InitializeComponent();
            this.Border.SizeChanged += (s, e) =>
              {
                  if (e.NewSize == e.PreviousSize) return;
                  this.Frame.Value = e.NewSize.Width;
                  this.Storyboard.Begin();
              };
        }
    }
}
