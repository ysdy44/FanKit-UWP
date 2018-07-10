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



        List<SampleCategory> SampleCategory = new List<SampleCategory>
        {

            new SampleCategory
            {
                Name="Brush",
                Samples=new List<Sample>
                {
                    new Sample( typeof(FanKit.Frames.Brush.SystemBrushPage),"SystemBrushes"),
                    new Sample(typeof(FanKit.Frames.Brush.LegacyBrushPage),"LegacyBrushes"),
                    new Sample(typeof(FanKit.Frames.Brush.InkToolbarBrushPage),"InkToolbarBrushes"),
                    new Sample(typeof(FanKit.Frames.Brush.OtherBrushPage),"OthersBrush"),
                    new Sample(typeof(FanKit.Frames.Brush.AcrylicElementBrushPage),"AcrylicElementBrushes"),
                    new Sample(typeof(FanKit.Frames.Brush.AcrylicWindowBrushPage),"AcrylicWindowBrushes"),
                    new Sample(typeof(FanKit.Frames.Brush.RevealBorderBrushPage),"RevealBorderBrushes"),
                    new Sample(typeof(FanKit.Frames.Brush.RevealBackgroundBrushPage),"RevealBackgroundBrushes"),
                    new Sample(typeof(FanKit.Frames.Brush.ColorPage),"Colors"),
                }
            },

            new SampleCategory
            {
                Name ="Style",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Style.TextblockStylePage),"TextblockStyle"),
                    new Sample(typeof(FanKit.Frames.Style.ButtonStylePage),"ButtonStyle"),
                }
            },

            new SampleCategory
            {
                Name ="Transition",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Transition.TransitionPage),"Transition"),
                }
            },

            new SampleCategory
            {
                Name="Template",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Template.FlyoutTemplatePage),"FlyoutTemplate"),
                    new Sample(typeof(FanKit.Frames.Template.TagChipPage),"TagChipControl"),
                    new Sample(typeof(FanKit.Frames.Template.FloatActionButtonPage),"FloatActionButton"),
                    new Sample(typeof(FanKit.Frames.Template.SplitPanelPage),"SplitPanel"),
                }
            },

            new SampleCategory
            {
                Name ="Control",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Control.PopupMenuPage),"PopupMenu"),
                    new Sample(typeof(FanKit.Frames.Control.TabBarPage),"TabBar"),
                    new Sample(typeof(FanKit.Frames.Control.AdaptiveSizePage),"AdaptiveSize"),
                    new Sample(typeof(FanKit.Frames.Control.TouchSliderPage),"TouchSlider"),
                }
            },

            new SampleCategory
            {
                Name ="Library",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Library.PalettePage),"Palette"),
                }
            },

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
                this.SampleCategoryControl.Category = category;
            }
        }


    }
}
