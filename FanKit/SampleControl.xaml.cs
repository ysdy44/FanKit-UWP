using FanKit.Samples;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FanKit
{
    public sealed partial class SampleControl : UserControl
    {

        #region DependencyProperty

        public Sample Sample
        {
            get { return (Sample)GetValue(SampleProperty); }
            set { SetValue(SampleProperty, value); }
        }
        public static readonly DependencyProperty SampleProperty = DependencyProperty.Register(nameof(Sample), typeof(Sample), typeof(SampleControl), new PropertyMetadata(0, (sender, e) =>
        {
            if (sender is SampleControl con)
            {
                if (e.NewValue is Sample value)
                {
                    con.ViewModel = value;
                }
            }
        }));

        #endregion

        public SampleControl()
        {
            this.InitializeComponent();
            this.Button.Tapped += (sender, e) => e.Handled = true;

            this.RootGrid.PointerEntered += (sender, e) => this.Entered();
            this.RootGrid.PointerExited += (sender, e) => this.Exited();
            this.RootGrid.PointerPressed += (sender, e) => this.Exited();
            this.RootGrid.PointerReleased += (sender, e) => this.Exited();
            this.RootGrid.PointerCanceled += (sender, e) => this.Exited();
        }

        #region Converter


        private string StateToStringConverter(SampleState state)
        {
            switch (state)
            {
                case SampleState.None:
                    return string.Empty;
                default:
                    return state.ToString();
            }
        }
        private Visibility StateToVisibilityConverter(SampleState state)
        {
            switch (state)
            {
                case SampleState.None:
                    return Visibility.Collapsed;
                default:
                    return Visibility.Visible;
            }
        }

        private SolidColorBrush StateToBackgroundConverter(SampleState state)
        {
            switch (state)
            {
                case SampleState.Disable:
                    return this.UnAccentColor;
                default:
                    return this.AccentColor;
            }
        }
        private SolidColorBrush StateToForegroundConverter(SampleState state)
        {
            switch (state)
            {
                case SampleState.Disable:
                    return this.UnCheckColor;
                default:
                    return this.CheckColor;
            }
        }


        #endregion

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
