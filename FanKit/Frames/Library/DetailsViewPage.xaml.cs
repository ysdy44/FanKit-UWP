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

namespace FanKit.Frames.Library
{
    public sealed partial class DetailsViewPage : Page
    {
        
        public static DetailsViewService Service = new DetailsViewService();

        public DetailsViewPage()
        {
            this.InitializeComponent();
        }

        /*     
        protected override void OnNavigatedTo(NavigationEventArgs e)=> Service.InitializeComponent(this.DetailFrame, typeof(DetailsView.WelcomePage));

        private void ListViewItemA_Tapped(object sender, TappedRoutedEventArgs e) => Service.Navigate(typeof(DetailsView.APage));
        private void ListViewItemB_Tapped(object sender, TappedRoutedEventArgs e) => Service.Navigate(typeof(DetailsView.BPage));
        private void ListViewItemC_Tapped(object sender, TappedRoutedEventArgs e) => Service.Navigate(typeof(DetailsView.CPage));
        private void ListViewItemD_Tapped(object sender, TappedRoutedEventArgs e) => Service.Navigate(typeof(DetailsView.DPage));

        private void Button_Tapped(object sender, TappedRoutedEventArgs e) => Service.GoBack();
*/

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Service.InitializeComponent(this.DetailFrame, typeof(DetailsView.WelcomePage));
        }
        private void ListViewItemA_Tapped(object sender, TappedRoutedEventArgs e) => Navigate(typeof(DetailsView.APage));
        private void ListViewItemB_Tapped(object sender, TappedRoutedEventArgs e) => Navigate(typeof(DetailsView.BPage));
        private void ListViewItemC_Tapped(object sender, TappedRoutedEventArgs e) => Navigate(typeof(DetailsView.CPage));
        private void ListViewItemD_Tapped(object sender, TappedRoutedEventArgs e) => Navigate(typeof(DetailsView.DPage));


        private void SettingButton_Tapped(object sender, TappedRoutedEventArgs e) => Navigate(typeof(DetailsView.PropertyPage));
        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Service.GoBack();
            Count();
        }


        private void Count()=> TextBlockRun.Text = this.DetailFrame.BackStack.Count().ToString();
        private void Navigate(Type page)
        {
            Service.Navigate(page);
            Count();
        }

    }
}
