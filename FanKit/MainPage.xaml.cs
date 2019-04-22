using FanKit.Samples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;

namespace FanKit
{
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Navigate:
        ///    Navigating Mainpage to a new page
        /// </summary>
        public static Action<Type> Navigate;
         
        //Methon
        public static Action<double> ImageButtonVisibleChange;
        private bool isImageVisible;
        public bool IsImageVisible
        {
            get => this.isImageVisible;
            set
            {
                if (value) this.NavigationView.Background = this.SolidColorBrush;
                else this.NavigationView.Background = this.ImageBrush;

                this.isImageVisible = value;
            }
        }
        
        private bool isCanGoBack;
        public bool IsCanGoBack
        {
            get => this.isCanGoBack;
            set
            {
                if (value)
                    this.BackButton.Content = "\uE0D5";//Back
                else
                    this.BackButton.Content = "\uE80F";//Home

                this.isCanGoBack = value;
            }
        }

        public MainPage()
        {
            this.InitializeComponent();                 
            MainPage.ImageButtonVisibleChange = (double offset) => this.sos.VerticalOffset = offset;  //ImageButtonVisible
            MainPage.Navigate = (page) => //Navigate
            {
                //Sample Category Control
                this.SamplesCategoryControl.Category = null;

                //Navigate
                this.NavigationFrame.Navigate(page);

                //Back
                this.IsCanGoBack = true;
            };

            this.Loaded += async (s, e) =>
            {
                //Frame          
                MainPage.Navigate(typeof(FanKit.Frames.Others.SplashPage));//页面跳转
                                                                     
                //Back   
                this.IsCanGoBack = false; 

                //SampleCategory
                string json = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Samples.json");
                this.ListView.ItemsSource = JsonConvert.DeserializeObject<List<SampleCategory>>(json);
            };
                                              

            {
                //TitleBar
                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;

                //TitleBar
                ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.BackgroundColor = titleBar.ButtonBackgroundColor = titleBar.ButtonInactiveBackgroundColor = Color.FromArgb(255, 0, 178, 240);
                titleBar.ButtonHoverBackgroundColor = titleBar.ButtonPressedBackgroundColor = Windows.UI.Colors.Gray;

                //Back按钮
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }

            //SampleCategory
            this.ListView.IsItemClickEnabled = true;
            this.ListView.ItemClick += (s, e) =>
            {
                this.BackButton.IsChecked = false;
                this.SettingButton.IsChecked = false;

                if (e.ClickedItem is SampleCategory category)
                {
                    if (this.SamplesCategoryControl.Category == category)
                        this.SamplesCategoryControl.Category = null;
                    else
                        this.SamplesCategoryControl.Category = category;
                }
            };


            //Button
            this.ImageVisibleButton.Tapped += (sender, e) => this.IsImageVisible = !this.IsImageVisible;
            this.SettingButton.Tapped += (s, e) => MainPage.Navigate(typeof(FanKit.Frames.Others.SettingPage));
            this.SamplesCategoryControl.ItemClick += (page) =>  MainPage.Navigate(page);
            this.BackButton.Tapped += (s, e) =>
            { 
                //Back
                if (this.NavigationFrame.CanGoBack) this.NavigationFrame.GoBack();

                 //Back
                 if (this.NavigationFrame.CanGoBack==false)
                {
                    this.SamplesCategoryControl.Category = null;

                    this.ListView.SelectedIndex = -1;
                 }

                 this.IsCanGoBack = this.NavigationFrame.CanGoBack;                  
             };
        }
    }
}



