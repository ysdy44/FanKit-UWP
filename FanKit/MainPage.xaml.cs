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

namespace FanKit
{
    public sealed partial class MainPage : Page
    {


        #region ImageButtonVisible：图片按钮可视


        //delegate
        public delegate void ImageButtonVisibleChangedHandler(double Offset);
        public static event ImageButtonVisibleChangedHandler ImageButtonVisibleChanged = null;
        
        //Methon
        public static void ImageButtonVisibleChange(double Offset) => ImageButtonVisibleChanged?.Invoke(Offset);

        //Property
        private double verticalOffset;
        public double VerticalOffset
        {
            get => verticalOffset;
            set
            {
                if (value < verticalOffset) this.Border.Visibility = Visibility.Visible;
                else if (value > verticalOffset) this.Border.Visibility = Visibility.Collapsed;

                verticalOffset = value;
            }
        }


        #endregion


        public MainPage()
        {
            this.InitializeComponent();
            this.ExtendAcrylicIntoTitleBar(); //扩展标题栏
        }


        #region Initialize：初始化


        //扩展标题栏
        private void ExtendAcrylicIntoTitleBar()
        {
            //扩展标题栏
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;

            //标题栏颜色
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            Color background= Color.FromArgb(255, 0, 178, 240);
            titleBar.BackgroundColor = background;
            titleBar.ButtonBackgroundColor = background;
            titleBar.ButtonInactiveBackgroundColor = background;

            titleBar.ButtonHoverBackgroundColor = Colors.Gray;
            titleBar.ButtonPressedBackgroundColor = Colors.Gray;

            //Back按钮
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FanKit.Frames.Others.SplashPage));//页面跳转

            //ImageButtonVisible：图片按钮可视
            FanKit.MainPage.ImageButtonVisibleChanged += (Offset) => this.VerticalOffset = Offset;
        }



        #endregion
        

        #region ImageVisible：图片可视


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
        

        #region Navigate：页面跳转


        private void HomeButton_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Others.SplashPage));//页面跳转
        private void SettingButton_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Others.SettingPage));//页面跳转
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.HomeButton.IsChecked = false;
            this.SettingButton.IsChecked = false;
        }


        private void SystemBrushItem_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Brush.SystemBrushPage));//页面跳转
        private void LegacyBrushItem_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Brush.LegacyBrushPage));//页面跳转
        private void InkToolbarBrushItem_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Brush.InkToolbarBrushPage));//页面跳转
        private void AcrylicElementBrushItem_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Brush.AcrylicElementBrushPage));//页面跳转
        private void AcrylicWindowBrushItem_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Brush.AcrylicWindowBrushPage));//页面跳转
        private void RevealBorderBrushItem_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Brush.RevealBorderPage));//页面跳转
        private void ColorItem_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Brush.ColorPage));//页面跳转
        private void TextBlockStylerItem_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Style.TextblockStylePage));//页面跳转
        private void ButtonStylerItem_Tapped(object sender, TappedRoutedEventArgs e) => this.Frame.Navigate(typeof(FanKit.Frames.Style.ButtonStylePage));//页面跳转




        #endregion

        
    }
}
