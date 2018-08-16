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

namespace FanKit
{
    public sealed partial class SampleControl : UserControl
    {

        public Sample Sample
        {
            get { return (Sample)GetValue(SampleProperty); }
            set { SetValue(SampleProperty, value); }
        }
        public static readonly DependencyProperty SampleProperty =  DependencyProperty.Register("Sample", typeof(Sample), typeof(SampleControl), new PropertyMetadata(0, (sender, e) =>
        {
            if (sender is SampleControl con)
            {
                if (e.NewValue is Sample  value)
                {
                    con.ViewModel = value;
                }
            }
        }));



        public SampleControl()
        {
            this.InitializeComponent();
        }

        private void Entered(object sender, PointerRoutedEventArgs e)
        { 
            OpacityAnimation animation = new OpacityAnimation() { To = 1, Duration = TimeSpan.FromMilliseconds(500) };
           animation.StartAnimation(this.DropShadowPanel);

           ScaleAnimation parentAnimation = new ScaleAnimation() { To = "1.6", Duration = TimeSpan.FromMilliseconds(500) };
           parentAnimation.StartAnimation(this.DropShadowPanel);
        }

        private void Exited(object sender, PointerRoutedEventArgs e)
        {
            OpacityAnimation animation = new OpacityAnimation() { To = 0, Duration = TimeSpan.FromMilliseconds(2000) };
            animation.StartAnimation(this.DropShadowPanel);

            ScaleAnimation parentAnimation = new ScaleAnimation() { To = "1", Duration = TimeSpan.FromMilliseconds(2000) };
           parentAnimation.StartAnimation(this.DropShadowPanel);
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e) => e.Handled = true;
      
    }
}
