using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

namespace FanKit.Frames.Colors
{
    public sealed partial class TouchSlider : UserControl
    {
        

        #region DependencyProperty


        public double Value { get => this.Slider.Value; set => this.Slider.Value = value; }
        public double Minimum { get => this.Slider.Minimum; set => this.Slider.Minimum = value; }
        public double Maximum { get => this.Slider.Maximum; set => this.Slider.Maximum = value; }

        public Brush SliderForeground { get => this.Slider.Foreground; set => this.Slider.Foreground = value; }
        public Brush SliderBackground { get => this.Slider.Background; set => this.Slider.Background = value; }
        

        /// <summary>
        /// <see cref="TouchSlider"/>'s IsStyle.
        /// </summary>
        public bool IsStyle
        {
            get { return (bool)GetValue(IsStyleProperty); }
            set { SetValue(IsStyleProperty, value); }
        }
        public static readonly DependencyProperty IsStyleProperty =DependencyProperty.Register(nameof(IsStyle), typeof(bool), typeof(TouchSlider), new PropertyMetadata(false, new PropertyChangedCallback(IsStyleOnChanged)));
        private static void IsStyleOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is TouchSlider con)
            {
                con.Slider.Style = ((bool)e.NewValue ? con.Resources["SliderStyle1"] : con.Resources["SliderStyle0"]) as Style;
            }
        }
      

        #endregion


        //event
        private RangeBaseValueChangedEventArgs e;
        public event RangeBaseValueChangedEventHandler ValueChangeStarted;
        public event RangeBaseValueChangedEventHandler ValueChangeDelta;
        public event RangeBaseValueChangedEventHandler ValueChangeCompleted;


        public TouchSlider()
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
