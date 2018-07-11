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


        #endregion

        #region SampleCategory：分类

        
        List<SampleCategory> SampleCategory = new List<SampleCategory>
        {

            new SampleCategory
            {
                Name="Brush",
                Samples=new List<Sample>
                {
                    new Sample( typeof(FanKit.Frames.Brush.SystemBrushPage),"SystemBrushes",new Uri("ms-appx:///Icon/Brush/SystemBrush.png")),
                    new Sample(typeof(FanKit.Frames.Brush.LegacyBrushPage),"LegacyBrushes",new Uri("ms-appx:///Icon/Brush/LegacyBrush.png")),
                    new Sample(typeof(FanKit.Frames.Brush.InkToolbarBrushPage),"InkToolbarBrushes",new Uri("ms-appx:///Icon/Brush/InkToolbarBrush.png")),
                    new Sample(typeof(FanKit.Frames.Brush.OtherBrushPage),"OthersBrush",new Uri("ms-appx:///Icon/Brush/OthersBrush.png")),
                    
                    new Sample(typeof(FanKit.Frames.Brush.AcrylicElementBrushPage),"AcrylicElementBrushes",new Uri("ms-appx:///Icon/Brush/AcrylicElementBrush.png")),
                    new Sample(typeof(FanKit.Frames.Brush.AcrylicWindowBrushPage),"AcrylicWindowBrushes",new Uri("ms-appx:///Icon/Brush/AcrylicWindowBrush.png")),

                    new Sample(typeof(FanKit.Frames.Brush.RevealBorderBrushPage),"RevealBorderBrushes",new Uri("ms-appx:///Icon/Brush/RevealBorderBrush.png")),
                    new Sample(typeof(FanKit.Frames.Brush.RevealBackgroundBrushPage),"RevealBackgroundBrushes",new Uri("ms-appx:///Icon/Brush/RevealBackgroundBrush.png")),

                    new Sample(typeof(FanKit.Frames.Brush.ColorPage),"Colors",new Uri("ms-appx:///Icon/Brush/Color.png")),
                }
            },

            new SampleCategory
            {
                Name ="Style",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Style.TextblockStylePage),"TextblockStyle",new Uri("ms-appx:///Icon/Style/TextBlockStyle.png")),
                    new Sample(typeof(FanKit.Frames.Style.ButtonStylePage),"ButtonStyle",new Uri("ms-appx:///Icon/Style/ButtonStyle.png")),
                }
            },
            
            new SampleCategory
            {
                Name ="Transition",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Transition.TransitionPage),"Transition",new Uri("ms-appx:///Icon/Transition/Transition.png")),
                }
            },

            new SampleCategory
            {
                Name="Template",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Template.FlyoutTemplatePage),"FlyoutTemplate",new Uri("ms-appx:///Icon/Template/FlyoutTemplate.png")),
                    new Sample(typeof(FanKit.Frames.Template.TagChipPage),"TagChipControl",new Uri("ms-appx:///Icon/Template/TagChip.png")),
                    new Sample(typeof(FanKit.Frames.Template.FloatActionButtonPage),"FloatActionButton",new Uri("ms-appx:///Icon/Template/FloatActionButton.png")),
                    new Sample(typeof(FanKit.Frames.Template.SplitPanelPage),"SplitPanel",new Uri("ms-appx:///Icon/Template/SplitPanel.png")),
                }
            },

            new SampleCategory
            {
                Name ="Control",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Control.PopupMenuPage),"PopupMenu",new Uri("ms-appx:///Icon/Control/PopupMenu.png")),
                    new Sample(typeof(FanKit.Frames.Control.TabBarPage),"TabBar",new Uri("ms-appx:///Icon/Control/TabBar.png")),
                    new Sample(typeof(FanKit.Frames.Control.AdaptiveSizePage),"AdaptiveSize",new Uri("ms-appx:///Icon/Control/AdaptiveSize.png")),
                    new Sample(typeof(FanKit.Frames.Control.TouchSliderPage),"TouchSlider",new Uri("ms-appx:///Icon/Control/TouchSlider.png")),
                }
            },

            new SampleCategory
            {
                Name ="Library",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Library.PalettePage),"Palette",new Uri("ms-appx:///Icon/Library/Palette.png")),
                }
            },

        };


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

            Color background = Color.FromArgb(255, 0, 178, 240);
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
            this.NavigationFrame.Navigate(typeof(FanKit.Frames.Others.SplashPage));//页面跳转

            //ImageButtonVisible：图片按钮可视
            FanKit.MainPage.ImageButtonVisibleChanged += (Offset) => this.sos.VerticalOffset = Offset;
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
        }


        #endregion


        //Sample Category Control
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.HomeButton.IsChecked = false;
            this.SettingButton.IsChecked = false;

            if (e.ClickedItem is SampleCategory category)
            {
                if (this.SampleCategoryControl.Category == category)
                {
                    this.SampleCategoryControl.Category = null;
                }
                else
                {
                    this.SampleCategoryControl.Category = category;
                }
            }
        }


    }
}
