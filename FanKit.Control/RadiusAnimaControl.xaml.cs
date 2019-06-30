﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Control
{
    /// <summary>
    /// The shadow panel of the control will also follow the animation, 
    /// if you change the width of the contents of the control.
    /// </summary>
    public sealed partial class RadiusAnimaPanel : UserControl
    {
        //@Content
        /// <summary> ContentBorder's Child. </summary>
        public UIElement CenterChild { get => this.ContentBorder.Child; set => this.ContentBorder.Child = value; }

        //@Construct
        public RadiusAnimaPanel()
        {
            this.InitializeComponent();
            this.ContentBorder.SizeChanged += (s, e) =>
              {
                  if (e.NewSize == e.PreviousSize) return;
                  this.Frame.Value = e.NewSize.Width;
                  this.Storyboard.Begin();
              };
        }
    }
}