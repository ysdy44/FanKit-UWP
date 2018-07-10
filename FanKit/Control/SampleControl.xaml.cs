using FanKit.Library;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Control
{
    public sealed partial class SampleControl : UserControl
    {

        public Sample sample;
        public Sample Sample
        {
            get => sample;
            set
            {
                this.TextBlock.Text = value.Name;
            }
        }


        public SampleControl()
        {
            this.InitializeComponent();
        }

        private void UserControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            OpacityAnimation animation = new OpacityAnimation() { To = 1, Duration = TimeSpan.FromMilliseconds(600) };
            animation.StartAnimation(this.DropShadowPanel);

            ScaleAnimation parentAnimation = new ScaleAnimation() { To = "1.6", Duration = TimeSpan.FromMilliseconds(600) };
            parentAnimation.StartAnimation(this.DropShadowPanel);
        }

        private void UserControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            OpacityAnimation animation = new OpacityAnimation() { To = 0, Duration = TimeSpan.FromMilliseconds(1200) };
            animation.StartAnimation(this.DropShadowPanel);

            ScaleAnimation parentAnimation = new ScaleAnimation() { To = "1", Duration = TimeSpan.FromMilliseconds(1200) };
            parentAnimation.StartAnimation(this.DropShadowPanel);
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Flyout.ShowAt(this);
            e.Handled = true;
        }

    }
}
