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
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.ApplicationModel;
using Windows.UI.Xaml.Hosting;
using System.Numerics;
using Windows.UI.Composition;
using Windows.UI.Core;

namespace FanKit.Frames.Template
{
    public sealed partial class TagChipPage : Page
    {
        public TagChipPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/TagChipXaml.txt");
            this.MarkdownText2.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Template/TagChipStyle.txt");
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is string s)
            {
                this.TexBlock.Text = s;
                this.TexBlockBorder.Visibility = Visibility.Visible;

                await Task.Delay(1500);
                this.TexBlockBorder.Visibility = Visibility.Collapsed;
            } 
        }

    } 
}
