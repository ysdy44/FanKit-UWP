using FanKit.Samples;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FanKit
{
    /// <summary>
    /// The mian page.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public bool IsImageVisible
        {
            get => this.BackgroundImage.Visibility == Visibility.Visible;
            set => this.BackgroundImage.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
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
        
        //@Construct
        public MainPage()
        {
            this.InitializeComponent();

            #region Sample

            Sample.ItemClick += (sender, page) =>
            {
                if (this.NavigationFrame.CurrentSourcePageType == page) return;

                //Navigate
                this.NavigationFrame.Navigate(page);
                this.SamplesCategoryControl.IsExpand = false;

                //Back
                this.IsCanGoBack = true;
            };

            Sample.FlyoutShow += (sender, sample) =>
            {
                //FlyoutSample
                FrameworkElement element = (FrameworkElement)sender;
                this.Billboard.CalculatePostion(element);
                this.Billboard.Sample = sample;

                this.BillboardCanvas.Visibility = Visibility.Visible;
            };

            this.BillboardCanvas.Tapped += (s, e) => this.BillboardCanvas.Visibility = Visibility.Collapsed;
            this.BillboardCanvas.Visibility = Visibility.Collapsed;

            #endregion

            this.Loaded += async (s, e) =>
            {
                Sample.ItemClick_Invoke(s, typeof(FanKit.Frames.Others.SplashPage));

                //Back   
                this.IsCanGoBack = false; 

                //SampleCategory
                string json = await FanKit.Samples.File.GetFile("ms-appx:///Samples/Samples.json");
                this.ListView.ItemsSource = JsonConvert.DeserializeObject<List<SamplesCategory>>(json);
            };
                                              

            {
                //TitleBar
                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;

                //TitleBar
                ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.BackgroundColor = titleBar.ButtonBackgroundColor = titleBar.ButtonInactiveBackgroundColor = Color.FromArgb(255, 0, 178, 240);
                titleBar.ButtonHoverBackgroundColor = titleBar.ButtonPressedBackgroundColor = Windows.UI.Colors.Gray;

                //BackButton
                SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
            }

            //SampleCategory
            this.ListView.IsItemClickEnabled = true;
            this.ListView.ItemClick += (s, e) =>
            {
                this.BackButton.IsChecked = false;
                this.SettingButton.IsChecked = false;

                if (e.ClickedItem is SamplesCategory category)
                {
                    this.SamplesCategoryControl.SetSampleCategory(category);
                }
            };


            //Button
            this.ImageVisibleButton.Tapped += (sender, e) => this.IsImageVisible = !this.IsImageVisible;
            this.SettingButton.Tapped += (s, e) =>
            {
                this.SamplesCategoryControl.IsExpand = false;

                Sample.ItemClick_Invoke(s, typeof(FanKit.Frames.Others.SettingPage));
            };
            this.BackButton.Tapped += (s, e) =>
            {
                //Back
                if (this.NavigationFrame.CanGoBack) this.NavigationFrame.GoBack();

                 //Back
                 if (this.NavigationFrame.CanGoBack==false)
                {
                    this.SamplesCategoryControl.IsExpand = false;

                    this.ListView.SelectedIndex = -1;
                 }

                 this.IsCanGoBack = this.NavigationFrame.CanGoBack;                  
             };
        }

    }
}