using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace FanKit.Frames.Control
{
    public sealed partial class TouchSliderControl : UserControl
    {
        //event
        private RangeBaseValueChangedEventArgs e;
        public event RangeBaseValueChangedEventHandler ValueChangeStarted;
        public event RangeBaseValueChangedEventHandler ValueChangeDelta;
        public event RangeBaseValueChangedEventHandler ValueChangeCompleted;


        public TouchSliderControl()
        {
            this.InitializeComponent();
        }


        //Value Changed
        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            this.e = e;
            this.ValueChangeDelta?.Invoke(sender, this.e);
        }

        //State Changed
        bool IsPressed = false;
        private void CommonStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            if (this.e != null)
            {
                if (e.NewState.Name == "Pressed")
                {
                    IsPressed = true;
                    this.ValueChangeStarted?.Invoke(sender, this.e);
                }

                if (e.NewState.Name != "Pressed")
                {
                    if (IsPressed == true)
                    {
                        IsPressed = false;
                        this.ValueChangeCompleted?.Invoke(sender, this.e);
                    }
                }
            }
        }


    }
}
