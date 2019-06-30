using FanKit.Samples;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FanKit
{
    /// <summary>
    /// Control of <see cref="FanKit.Samples.Sample">.
    /// </summary>
    public sealed partial class SampleControl : UserControl
    {

        //@Converter
        private string StateToStringConverter(SampleState state) => (state == SampleState.None) ? string.Empty : state.ToString();
        private Visibility StateToVisibilityConverter(SampleState state) => (state == SampleState.None) ? Visibility.Collapsed : Visibility.Visible;
        private SolidColorBrush StateToBackgroundConverter(SampleState state) => (state == SampleState.Disable) ? this.UnAccentColor : this.AccentColor;
        private SolidColorBrush StateToForegroundConverter(SampleState state) => (state == SampleState.Disable) ? this.UnCheckColor : this.CheckColor;
        
        #region DependencyProperty

        /// <summary> DataContext </summary>
        public Sample Sample
        {
            get { return (Sample)GetValue(SampleProperty); }
            set { SetValue(SampleProperty, value); }
        }
        /// <summary> Identifies the <see cref = "SampleControl.Sample" /> dependency property. </summary>
        public static readonly DependencyProperty SampleProperty = DependencyProperty.Register(nameof(Sample), typeof(Sample), typeof(SampleControl), new PropertyMetadata(new Sample()));

        #endregion
        
        //@Construct
        public SampleControl()
        {            
            this.InitializeComponent();
            this.Button.Tapped += (s, e) => e.Handled = true;

            this.RootGrid.PointerEntered += (s, e) => this.Entered();
            this.RootGrid.PointerExited += (s, e) => this.Exited();
            this.RootGrid.PointerPressed += (s, e) => this.Exited();
            this.RootGrid.PointerReleased += (s, e) => this.Exited();
            this.RootGrid.PointerCanceled += (s, e) => this.Exited();
        }


        private void Entered()
        {
            OpacityAnimation animation = new OpacityAnimation() { To = 1, Duration = TimeSpan.FromMilliseconds(500) };
            animation.StartAnimation(this.DropShadowPanel);

            ScaleAnimation parentAnimation = new ScaleAnimation() { To = "1.6", Duration = TimeSpan.FromMilliseconds(500) };
            parentAnimation.StartAnimation(this.DropShadowPanel);
        }
        private void Exited()
        {
            OpacityAnimation animation = new OpacityAnimation() { To = 0, Duration = TimeSpan.FromMilliseconds(2000) };
            animation.StartAnimation(this.DropShadowPanel);

            ScaleAnimation parentAnimation = new ScaleAnimation() { To = "1", Duration = TimeSpan.FromMilliseconds(2000) };
            parentAnimation.StartAnimation(this.DropShadowPanel);
        }
    }
}