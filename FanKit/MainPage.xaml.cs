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
        
        #region ImageButtonVisible


        //delegate
        public delegate void ImageButtonVisibleChangedHandler(double Offset);
        public static event ImageButtonVisibleChangedHandler ImageButtonVisibleChanged = null;

        //Methon
        public static void ImageButtonVisibleChange(double Offset) => ImageButtonVisibleChanged?.Invoke(Offset);


        #endregion

        #region SampleCategory

        
        List<SampleCategory> SampleCategory = new List<SampleCategory>
        {

            new SampleCategory
            {
                Name="Brushes",
                Samples=new List<Sample>
                {
                    new Sample( typeof(FanKit.Frames.Brushes.SystemBrushPage),"SystemBrushes",new Uri("ms-appx:///Icon/Brushes/SystemBrush.png")),
                    new Sample(typeof(FanKit.Frames.Brushes.LegacyBrushPage),"LegacyBrushes",new Uri("ms-appx:///Icon/Brushes/LegacyBrush.png")),
                    new Sample(typeof(FanKit.Frames.Brushes.InkToolbarBrushPage),"InkToolbarBrushes",new Uri("ms-appx:///Icon/Brushes/InkToolbarBrush.png")),
                    new Sample(typeof(FanKit.Frames.Brushes.OtherBrushPage),"OthersBrush",new Uri("ms-appx:///Icon/Brushes/OthersBrush.png")),
                    
                    new Sample(typeof(FanKit.Frames.Brushes.AcrylicElementBrushPage),"AcrylicElementBrushes",new Uri("ms-appx:///Icon/Brushes/AcrylicElementBrush.png")),
                    new Sample(typeof(FanKit.Frames.Brushes.AcrylicWindowBrushPage),"AcrylicWindowBrushes",new Uri("ms-appx:///Icon/Brushes/AcrylicWindowBrush.png")),

                    new Sample(typeof(FanKit.Frames.Brushes.RevealBorderBrushPage),"RevealBorderBrushes",new Uri("ms-appx:///Icon/Brushes/RevealBorderBrush.png")),
                    new Sample(typeof(FanKit.Frames.Brushes.RevealBackgroundBrushPage),"RevealBackgroundBrushes",new Uri("ms-appx:///Icon/Brushes/RevealBackgroundBrush.png")),

                    new Sample(typeof(FanKit.Frames.Brushes.ColorPage),"Colors",new Uri("ms-appx:///Icon/Brushes/Color.png")),
                }
            },

            new SampleCategory
            {
                Name ="Style",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Styles.TextblockStylePage),"TextblockStyle",new Uri("ms-appx:///Icon/Styles/TextBlockStyle.png")),
                    new Sample(typeof(FanKit.Frames.Styles.ButtonStylePage),"ButtonStyle",new Uri("ms-appx:///Icon/Styles/ButtonStyle.png")),
                }
            },
            
            new SampleCategory
            {
                Name ="Helpers",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Helpers.TransitionPage),"Transition",new Uri("ms-appx:///Icon/Helpers/Transition.png")),
                    new Sample(typeof(FanKit.Frames.Helpers.StretchPage),"Stretch",new Uri("ms-appx:///Icon/Helpers/Stretch.png")),
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
                    new Sample(typeof(FanKit.Frames.Control.ExpandTextViewPage),"ExpandTextView",new Uri("ms-appx:///Icon/Control/ExpandTextView.png")),
                }
            },

            new SampleCategory
            {
                Name ="Library",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Library.PalettePage),"Palette",new Uri("ms-appx:///Icon/Library/Palette.png")),
                    new Sample(typeof(FanKit.Frames.Library.DataTemplateAdaptiverPage),"DataTemplateAdaptiver",new Uri("ms-appx:///Icon/Library/DataTemplateAdaptiver.png")),
                    new Sample(typeof(FanKit.Frames.Library.DetailsViewPage),"DetailsView",new Uri("ms-appx:///Icon/Library/DetailsView.png")),
                    new Sample(typeof(FanKit.Frames.Library.ScalableGridPage),"ScalableGrid",new Uri("ms-appx:///Icon/Library/ScalableGrid.png")),
                }
            },

            new SampleCategory
            {
                Name ="Colors",
                Samples=new List<Sample>
                {
                    new Sample(typeof(FanKit.Frames.Colors.NumberPickerPage),"NumberPicker",new Uri("ms-appx:///Icon/Colors/NumberPicker.png")),
                    new Sample(typeof(FanKit.Frames.Colors.TouchSliderPage),"TouchSlider",new Uri("ms-appx:///Icon/Colors/TouchSlider.png")),
                    new Sample(typeof(FanKit.Frames.Colors.RGBPickerPage),"RGBPicker",new Uri("ms-appx:///Icon/Colors/RGBPicker.png")),
                    new Sample(typeof(FanKit.Frames.Colors.HSLPickerPage),"HSLPicker",new Uri("ms-appx:///Icon/Colors/HSLPicker.png")),
                }
            },

        };


        #endregion
        

        public MainPage()
        {
            this.InitializeComponent();
            this.ExtendAcrylicIntoTitleBar(); //扩展标题栏
        }


        #region Initialize


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


        //Sample Category Control
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.BackButton.IsChecked = false;
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



