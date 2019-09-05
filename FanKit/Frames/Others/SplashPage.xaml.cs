using FanKit.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Others
{
    /// <summary>
    /// Page of splash.
    /// </summary>
    public sealed partial class SplashPage : Page
    { 

        List<Type> _pages = new List<Type>
        {
             typeof(FanKit.Frames.Transformers.CurveNodesPage),
             typeof(FanKit.Frames.Transformers.DottedLinePage),
             typeof(FanKit.Frames.Transformers.MarqueeToolPage),
             typeof(FanKit.Frames.Transformers.DottedLineAndMarqueeToolPage),
          };

        //@Construct
        public SplashPage()
        {
            this.InitializeComponent();

            this.ItemsControl.ItemsSource = (from page in this._pages select this.BuildUIElement(page)).ToList();

            this.SizeChanged += (s, e) =>
              {
                  if (e.NewSize == e.PreviousSize) return;

                  if (e.NewSize.Width > 600)
                  {
                      Grid.SetColumn(this.IconPanel, 0);
                      Grid.SetRow(this.IconPanel, 0);
                      Grid.SetColumn(this.NewPanel, 2);
                      Grid.SetRow(this.NewPanel, 0);
                  }
                  else
                  {
                      Grid.SetColumn(this.IconPanel, 0);
                      Grid.SetRow(this.IconPanel, 0);
                      Grid.SetColumn(this.NewPanel, 0);
                      Grid.SetRow(this.NewPanel, 2);
                  }
              };
         }
                     

        private UIElement BuildUIElement(Type page)
        {
            string[] splits = page.ToString().Split('.');
            string name = splits.Last();

            HyperlinkButton hyperlinkButton = new HyperlinkButton
            {
                Content = splits[splits.Length - 2] + ">" + name.Remove(name.Length-4)
            };

            hyperlinkButton.Tapped += (sender, e) => Sample.NavigatePage_Invoke(this, page);//Delegate

            return hyperlinkButton;
        }

    }
}