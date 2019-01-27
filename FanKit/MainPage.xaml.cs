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
using Newtonsoft.Json;
using Windows.Storage;
using Windows.System;

namespace FanKit
{
    public sealed partial class MainPage : Page
    {   
        //Navigate
        public static Action<Type> Navigate;

        //delegate
        public delegate void ImageButtonVisibleChangedHandler(double Offset);
        public static event ImageButtonVisibleChangedHandler ImageButtonVisibleChanged = null;
     
        //Methon
        public static void ImageButtonVisibleChange(double Offset) => ImageButtonVisibleChanged?.Invoke(Offset);
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

        public MainPage()
        {
            this.InitializeComponent();


            //delegate
            this.ImageVisibleButton.Tapped += (sender, e) => this.IsImageVisible = !this.IsImageVisible;


            //Navigate
            MainPage.Navigate = (page) =>
            {
                //Sample Category Control
                this.SampleCategoryControl.Category = null;

                //Navigate
                this.ListView.SelectedIndex = -1;

                //Navigate
                this.NavigationFrame.Navigate(page);

                //Back
                this.BackButton.Content = "\uE0D5";
            };


            // this.ExtendAcrylicIntoTitleBar(); //TitleBar
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


            this.Loaded += async (sender, e) =>
            {
                //Frame          
                MainPage.Navigate(typeof(FanKit.Frames.Others.SplashPage));//页面跳转

                //ImageButtonVisible
                FanKit.MainPage.ImageButtonVisibleChanged += (Offset) => this.sos.VerticalOffset = Offset;

                //SampleCategory
                string json = await FanKit.Library.File.GetFile("ms-appx:///TXT/Samples.json");
                this.ListView.ItemsSource = JsonConvert.DeserializeObject<List<SampleCategory>>(json);
            };

        }




        private void HomeButton_Tapped(object sender, TappedRoutedEventArgs e) => MainPage.Navigate(typeof(FanKit.Frames.Others.SplashPage));
        private void SettingButton_Tapped(object sender, TappedRoutedEventArgs e) => MainPage.Navigate(typeof(FanKit.Frames.Others.SettingPage));
        private void SampleCategoryControl_ItemClick(Type page) => MainPage.Navigate(page);
        private void BackButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //Back
            if (this.NavigationFrame.CanGoBack) this.NavigationFrame.GoBack();

            //Back
            if (this.NavigationFrame.CanGoBack) this.BackButton.Content = "\uE0D5";
            else this.BackButton.Content = "\uE80F";
        }



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



