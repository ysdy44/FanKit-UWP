using System.Numerics;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Win2Ds
{
    public sealed partial class DottedLinePage : Page
    {
        //Pointer
        Vector2 canvasStartingPoint = new Vector2();

        public DottedLinePage()
        {
            this.InitializeComponent();
            
        }
    }
}