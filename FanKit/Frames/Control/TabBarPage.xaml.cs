using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


namespace FanKit.Frames.Control
{
    public sealed partial class TabBarPage : Page
    {

        public TabBarPage()
        {
            this.InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Control/TabBarXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Control/TabBarUserXaml.txt");
            this.MarkdownText3.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Control/TabBarUserCs.txt");

            this.sos.ShowChanged += (IsShow) =>
            {
                if (IsShow) this.SpliteFade.Begin();
                else this.SpliteShow.Begin(); 
            };
        }



        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (this.TabPivot.SelectedIndex == 4) this.TabPivot.SelectedIndex = 0;
            else this.TabPivot.SelectedIndex++;
        }

    }
}
