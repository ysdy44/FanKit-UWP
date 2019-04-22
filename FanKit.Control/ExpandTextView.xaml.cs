using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace FanKit.Control
{
    public sealed partial class ExpandTextView : UserControl
    {
        //Delegate
        public delegate void ExpandChangedHandler(bool IsExpand);
        public event ExpandChangedHandler ExpandChanged = null;

        #region DependencyProperty


        /// <summary> <see cref="ExpandTextView"/>'s text. </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(ExpandTextView), new PropertyMetadata(string.Empty, new PropertyChangedCallback((sender, e) =>
        {
            ExpandTextView con = (ExpandTextView)sender;

            if (e.NewValue is string text)
            {
                con.OriginTextBlock.Text = text;
                con.TrimTextBlock.Text = text;

                con.RootGrid.Height = con.DefaultHeight;
            }
        })));


        /// <summary> <see cref="ExpandTextView"/>'s minimum height after UnExpand. </summary> 
        public double DefaultHeight
        {
            get { return (double)GetValue(DefaultHeightProperty); }
            set { SetValue(DefaultHeightProperty, value); }
        }
        public static readonly DependencyProperty DefaultHeightProperty = DependencyProperty.Register(nameof(DefaultHeight), typeof(double), typeof(ExpandTextView), new PropertyMetadata(70d, new PropertyChangedCallback((sender, e) =>
        {
            ExpandTextView con = (ExpandTextView)sender;

            if (e.NewValue is double value)
            {
                con.IsExpand = false;
                con.RootGrid.Height = value;
            }
        })));


        /// <summary> If true ,the height of this control is the <see cref="DefaultHeight"/>. </summary>
        public bool IsExpand
        {
            get { return (bool)GetValue(IsExpandProperty); }
            set { SetValue(IsExpandProperty, value); }
        }
        public static readonly DependencyProperty IsExpandProperty = DependencyProperty.Register(nameof(IsExpand), typeof(bool), typeof(ExpandTextView), new PropertyMetadata(false, new PropertyChangedCallback((sender, e) =>
        {
            ExpandTextView con = (ExpandTextView)sender;

            if (e.NewValue is bool value)
            {
                if (e.OldValue is bool oldValue)
                {
                    if (value != oldValue)
                    {
                        if (con.OriginTextBlock.ActualHeight != double.NaN)
                        {
                            if (con.OriginTextBlock.ActualHeight > con.DefaultHeight)
                            {
                                if (value)
                                {
                                    DoubleAnimation animation = new DoubleAnimation { Duration = new Duration(TimeSpan.FromSeconds(0.2)), From = con.DefaultHeight, To = con.OriginTextBlock.ActualHeight, EnableDependentAnimation = true };
                                    Storyboard.SetTarget(animation, con.RootGrid);
                                    Storyboard.SetTargetProperty(animation, "(UIElement.Height)");
                                    Storyboard storyBoard = new Storyboard();
                                    storyBoard.Children.Add(animation);
                                    storyBoard.Begin();
                                    con.OriginTextBlock.Opacity = 1;
                                    con.TrimTextBlock.Opacity = 0;
                                    return;
                                }
                                else
                                {
                                    DoubleAnimation animation = new DoubleAnimation { Duration = new Duration(TimeSpan.FromSeconds(0.2)), From = con.OriginTextBlock.ActualHeight, To = con.DefaultHeight, EnableDependentAnimation = true };
                                    animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
                                    Storyboard.SetTarget(animation, con.RootGrid);
                                    Storyboard.SetTargetProperty(animation, "(UIElement.Height)");
                                    Storyboard storyBoard = new Storyboard();
                                    storyBoard.Children.Add(animation);
                                    storyBoard.Begin();
                                    con.OriginTextBlock.Opacity = 0;
                                    con.TrimTextBlock.Opacity = 1;
                                    return;
                                }
                            }
                        }

                        con.RootGrid.Height = con.DefaultHeight;
                    }
                }
            }
        })));


        #endregion
        
        public ExpandTextView()
        {
            this.InitializeComponent();
            this.Tapped += (s, e) =>
            {
                if (this.IsExpand)
                {
                    this.IsExpand = false;
                    this.ExpandChanged?.Invoke(false);//Delegate
                }
                else
                {
                    this.IsExpand = true;
                    this.ExpandChanged?.Invoke(true);//Delegate
                }
            };
        }
    }
}