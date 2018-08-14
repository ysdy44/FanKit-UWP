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


namespace FanKit.Frames.Helpers
{
    public sealed partial class StretchPage : Page
    {
        public StretchPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<Image  Stretch=\"";
            this.TopRun2.Text = "None";
            this.TopRun3.Text = "\" /> ";
         }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
