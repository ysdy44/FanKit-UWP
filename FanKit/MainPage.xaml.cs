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
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using FanKit.Library;
using MaterialLibs;
using Newtonsoft.Json;
using Windows.Storage;
using Windows.System;

namespace FanKit
{
    public sealed partial class MainPage : Page
    {
        //delegate
        public delegate void ImageButtonVisibleChangedHandler(double Offset);
        public static event ImageButtonVisibleChangedHandler ImageButtonVisibleChanged = null;
        //Methon
        public static void ImageButtonVisibleChange(double Offset) => ImageButtonVisibleChanged?.Invoke(Offset);
         
        
        public MainPage()
        {
            this.InitializeComponent();
            this.ExtendAcrylicIntoTitleBar(); //TitleBar
        }


        #region Initialize


        //TitleBar
        private void ExtendAcrylicIntoTitleBar()
        {
            //TitleBar
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;

            //TitleBar
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            Color background = Color.FromArgb(255, 0, 178, 240);
            titleBar.BackgroundColor = background;
            titleBar.ButtonBackgroundColor = background;
            titleBar.ButtonInactiveBackgroundColor = background;

            titleBar.ButtonHoverBackgroundColor = Colors.Gray;
            titleBar.ButtonPressedBackgroundColor = Colors.Gray;

            //Back按钮
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.NavigationFrame.Navigate(typeof(FanKit.Frames.Others.SplashPage));//页面跳转
 
            //ImageButtonVisible
            FanKit.MainPage.ImageButtonVisibleChanged += (Offset) => this.sos.VerticalOffset = Offset;

            //SampleCategory
            string json = await FanKit.Library.File.GetFile("ms-appx:///TXT/Samples.json");
            this.ListView.ItemsSource = JsonConvert.DeserializeObject<List<SampleCategory>>(json);
        }



        #endregion


        #region ImageVisible


        private bool isImageVisible;
        public bool IsImageVisible
        {
            get => isImageVisible;
            set
            {
                if (value) this.NavigationView.Background = this.SolidColorBrush;
                else this.NavigationView.Background = this.ImageBrush;

                isImageVisible = value;
            }
        }
        private void Border_Tapped(object sender, TappedRoutedEventArgs e) => this.IsImageVisible = !this.IsImageVisible;


        #endregion


        #region Navigate


        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {         
            //Back
            if (this.NavigationFrame.CanGoBack)  this.NavigationFrame.GoBack();

            //Back
            if (this.NavigationFrame.CanGoBack) this.BackButton.Content = "\uE0D5";
            else this.BackButton.Content = "\uE80F";
        }
        private void HomeButton_Tapped(object sender, TappedRoutedEventArgs e) => this.Navigate(typeof(FanKit.Frames.Others.SplashPage));
        private void SettingButton_Tapped(object sender, TappedRoutedEventArgs e) => this.Navigate(typeof(FanKit.Frames.Others.SettingPage));
        private void SampleCategoryControl_ItemClick(Type page) => this.Navigate(page);

        private void Navigate(Type page)
        {
            //Sample Category Control
            this.SampleCategoryControl.Category = null;

            //Navigate
            this.ListView.SelectedIndex = -1;
    
            //Navigate
            this.NavigationFrame.Navigate(page);

            //Back
            this.BackButton.Content = "\uE0D5";
        }


        #endregion


        //SampleCategory
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.BackButton.IsChecked = false;
            this.SettingButton.IsChecked = false;

            if (e.ClickedItem is SampleCategory category)
            {
                if (this.SampleCategoryControl.Category == category)
                    this.SampleCategoryControl.Category = null;
                else
                    this.SampleCategoryControl.Category = category;
            }
        }
    }
}



