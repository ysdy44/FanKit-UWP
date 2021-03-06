﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Others
{
    /// <summary>
    /// Page of setting.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
            this.Loaded += (s, e) =>
            {
                if (Window.Current.Content is FrameworkElement frameworkElement)
                {
                    ElementTheme theme = frameworkElement.RequestedTheme;
                    this.LightRadioButton.IsChecked = (theme == ElementTheme.Light);
                    this.DarkRadioButton.IsChecked = (theme == ElementTheme.Dark);
                    this.DefaultRadioButton.IsChecked = (theme == ElementTheme.Default);
                }
            };

            this.LightRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Light);
            this.DarkRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Dark);
            this.DefaultRadioButton.Checked += (s, e) => this.SetTheme(ElementTheme.Default);
        }

        private void SetTheme(ElementTheme requestedTheme)
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                if (frameworkElement.RequestedTheme == requestedTheme) return;

                frameworkElement.RequestedTheme = requestedTheme;
            }
        }
    }
}