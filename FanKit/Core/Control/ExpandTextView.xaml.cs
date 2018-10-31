using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace FanKit.Frames.Control
{
    public sealed partial class ExpandTextView : UserControl
    {

        #region DependencyProperty


        /// <summary>
        /// <see cref="ExpandTextView"/>'s text.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(ExpandTextView), new PropertyMetadata(string.Empty, new PropertyChangedCallback(TextOnChanged)));
        private static void TextOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ExpandTextView con) con.SetText((string)e.NewValue);
        }
        private void SetText(string value)
        {
            this.OriginTextBlock.Text = value;
            this.TrimTextBlock.Text = value;

            this.Grid.Height = this.DefaultHeight;
        }


        /// <summary>
        /// <see cref="ExpandTextView"/>'s minimum height after UnExpand.
        /// </summary> 
        public double DefaultHeight
        {
            get { return (double)GetValue(DefaultHeightProperty); }
            set { SetValue(DefaultHeightProperty, value); }
        }
        public static readonly DependencyProperty DefaultHeightProperty = DependencyProperty.Register(nameof(DefaultHeight), typeof(double), typeof(ExpandTextView), new PropertyMetadata(70d, new PropertyChangedCallback(DefaultHeightOnChanged)));
        private static void DefaultHeightOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ExpandTextView con) con.SetIsDefaultHeight((double)e.NewValue);
        }
        private void SetIsDefaultHeight(double value)
        {
            this.IsExpand = false;

            this.Grid.Height = value;
        }

        /// <summary>
        ///If true ,the height of this control is the <see cref="DefaultHeight"/> .
        /// </summary>
        public bool IsExpand
        {
            get { return (bool)GetValue(IsExpandProperty); }
            set { SetValue(IsExpandProperty, value); }
        }
        public static readonly DependencyProperty IsExpandProperty =
            DependencyProperty.Register(nameof(IsExpand), typeof(bool), typeof(ExpandTextView), new PropertyMetadata(false, new PropertyChangedCallback(IsExpandOnChanged)));
        private static void IsExpandOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is ExpandTextView con)
            {
                if (e.NewValue is bool value)
                {
                    if (value != (bool)e.OldValue)
                    {
                        con.SetIsExpand(value);
                    }
                }
            }
        }
        private void SetIsExpand(bool value)
        {
            if (this.OriginTextBlock.ActualHeight != double.NaN)
            {
                if (this.OriginTextBlock.ActualHeight > this.DefaultHeight)
                {
                    if (value)
                    {
                        DoubleAnimation animation = new DoubleAnimation
                        {
                            Duration = new Duration(TimeSpan.FromSeconds(0.2)),
                            From = this.DefaultHeight,
                            To = this.OriginTextBlock.ActualHeight,
                            EnableDependentAnimation = true
                        };

                        Storyboard.SetTarget(animation, this.Grid);
                        Storyboard.SetTargetProperty(animation, "(UIElement.Height)");

                        Storyboard storyBoard = new Storyboard();
                        storyBoard.Children.Add(animation);
                        storyBoard.Begin();

                        this.OriginTextBlock.Opacity = 1;
                        this.TrimTextBlock.Opacity = 0;

                        return;
                    }
                    else
                    {
                        DoubleAnimation animation = new DoubleAnimation
                        {
                            Duration = new Duration(TimeSpan.FromSeconds(0.2)),
                            From = this.OriginTextBlock.ActualHeight,
                            To = this.DefaultHeight,
                            EnableDependentAnimation = true
                        };
                        animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };

                        var hiuhi = new SplineDoubleKeyFrame();
                        Storyboard.SetTarget(animation, this.Grid);
                        Storyboard.SetTargetProperty(animation, "(UIElement.Height)");

                        Storyboard storyBoard = new Storyboard();
                        storyBoard.Children.Add(animation);
                        storyBoard.Begin();

                        this.OriginTextBlock.Opacity = 0;
                        this.TrimTextBlock.Opacity = 1;

                        return;
                    }
                }
            }

            this.Grid.Height = this.DefaultHeight;
        }


        #endregion

        //delegate
        public delegate void ExpandChangedHandler(bool IsExpand);
        public event ExpandChangedHandler ExpandChanged = null;

        public ExpandTextView()
        {
            this.InitializeComponent(); 
        }

        private void UserControl_Tapped(object sender, Windows.UI.Xaml.Input.TappedRoutedEventArgs e)
        {
            if (this.IsExpand)
            {
                this.IsExpand = false;
                this.ExpandChanged?.Invoke(false);
            }
            else
            {
                this.IsExpand = true;
                this.ExpandChanged?.Invoke(true);
            }
        }
    }
}








