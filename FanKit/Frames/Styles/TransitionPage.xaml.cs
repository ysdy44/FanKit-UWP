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

namespace FanKit.Frames.Styles
{
    public sealed partial class TransitionPage : Page
    {
        Random _random = new Random();

        public TransitionPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Styles/Transition.style.txt");
            };
            
            this.EntranceButton.Tapped += (s, e) => this.EntranceContentControl.Content = new TextBlock
            {
                Text = "TextBlock"
            };
            this.ContentButton.Tapped += (s, e) => this.ContentContentControl.Content = new TextBlock
            {
                Text = "TextBlock"
            };
            this.RepositionButton.Tapped += (s, e) => this.RepositionRectangle.Margin = (this.RepositionRectangle.Margin == new Thickness(0)) ? new Thickness(100,100,0,0) : this.RepositionRectangle.Margin = new Thickness(0);
            this.PopupButton.Tapped += (s, e) => this.PopupPopup.IsOpen = this.PopupPopup.IsOpen ? false : true;
            this.PaneButton.Tapped += (s, e) => this.PanePopup.IsOpen = this.PanePopup.IsOpen ? false : true;
            this.EdgeUIButton.Tapped += (s, e) => this.EdgeUIPopup.IsOpen = this.EdgeUIPopup.IsOpen ? false : true;
        }
    }
}