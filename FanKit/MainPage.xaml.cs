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


        List<PageType> pages = new List<PageType>
        {
         new PageType( typeof(FanKit.Frames.Brush.SystemBrushPage),"SystemBrushes"),
           new PageType(typeof(FanKit.Frames.Brush.LegacyBrushPage),"LegacyBrushes"),
           new PageType(typeof(FanKit.Frames.Brush.InkToolbarBrushPage),"InkToolbarBrushes"),
           new PageType(typeof(FanKit.Frames.Brush.OtherBrushPage),"OthersBrush"),

            new PageType(typeof(FanKit.Frames.Brush.AcrylicElementBrushPage),"AcrylicElementBrushes"),
           new PageType(typeof(FanKit.Frames.Brush.AcrylicWindowBrushPage),"AcrylicWindowBrushes"),
           new PageType(typeof(FanKit.Frames.Brush.RevealBorderBrushPage),"RevealBorderBrushes"),
           new PageType(typeof(FanKit.Frames.Brush.RevealBackgroundBrushPage),"RevealBackgroundBrushes"),


            new PageType(typeof(FanKit.Frames.Brush.ColorPage),"Colors"),


            new PageType(typeof(FanKit.Frames.Style.TextblockStylePage),"TextblockStyle"),
            new PageType(typeof(FanKit.Frames.Style.ButtonStylePage),"ButtonStyle"),

            new PageType(typeof(FanKit.Frames.Template.FlyoutTemplatePage),"FlyoutTemplate"),
            new PageType(typeof(FanKit.Frames.Template.TagChipPage),"TagChipControl"),
            new PageType(typeof(FanKit.Frames.Template.FloatActionButtonPage),"FloatActionButton"),
            new PageType(typeof(FanKit.Frames.Template.SplitPanelPage),"SplitPanel"),
        };

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

            if (e.ClickedItem is PageType pages)
            {
                this.Frame.Navigate(pages.Page);//页面跳转
            }    
        }


        #endregion


    }
    public class PageType
    {
        public Type Page;
        public string Name;

        public PageType(Type page, string name)
        {
            this.Page = page;
            this.Name = name;
        }
    }

}
