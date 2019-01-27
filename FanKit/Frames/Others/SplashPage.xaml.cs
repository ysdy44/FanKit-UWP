using FanKit.Library;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace FanKit.Frames.Others
{
    public sealed partial class SplashPage : Page
    { 

        List<Type> Pages = new List<Type>
        {
            typeof(FanKit.Frames.Win2Ds.TransformControllerPage),
            typeof(FanKit.Frames.Win2Ds.MarqueeToolPage),
            typeof(FanKit.Frames.Colors.ColorPickerPage),
            typeof(FanKit.Frames.Colors.TouchSliderPage),
            typeof(FanKit.Frames.Colors.StrawPickerPage),
            typeof(FanKit.Frames.Control.RadiusAnimaPanelPage),
            typeof(FanKit.Frames.Helpers.ColorsPage),
         };

        public SplashPage()
        {
            this.InitializeComponent();

            this.ItemsControl.ItemsSource = (from page in this.Pages select this.BuildUIElement(page)).ToList();

            this.SizeChanged += (sender, e) =>
              {
                  if (e.NewSize.Width>600)
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

            hyperlinkButton.Tapped += (sender, e) => MainPage.Navigate(page);

            return hyperlinkButton;
        }


    }
}
