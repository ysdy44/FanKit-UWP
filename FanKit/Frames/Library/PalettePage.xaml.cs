using System;
using System.Collections.Generic;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Library
{
    public sealed partial class PalettePage : Page
    {

        List<Uri> list = new List<Uri>
            {
                new Uri("ms-appx:///Icon/Photos/BisonBadlandsChillin.jpg"),
                new Uri("ms-appx:///Icon/Photos/ColumbiaRiverGorge.jpg"),

                new Uri("ms-appx:///Icon/Photos/GrandTetons.jpg"),
                new Uri("ms-appx:///Icon/Photos/MilkyWayStHelensHikePurple.jpg"),

                new Uri("ms-appx:///Icon/Photos/NorthernCascadesReflection.jpg"),
                new Uri("ms-appx:///Icon/Photos/Owl.jpg"),

                new Uri("ms-appx:///Icon/Photos/ShootingOnAutoOnTheDrone.jpg"),
                new Uri("ms-appx:///Icon/Photos/SmithnRockDownTheRiverView.jpg"),

                new Uri("ms-appx:///Icon/Photos/SnowyInterbayt.jpg"),
                new Uri("ms-appx:///Icon/Photos/SpeedTripleAtristsPoint.jpg"),
            };

        public PalettePage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e) => SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = this.Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.MarkdownText1.Text = await FanKit.Library.File.GetFile("ms-appx:///TXT/Library/Palette.txt");
        }


        private async void CarouselControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = this.CarouselControl.SelectedIndex;
            Uri uri = this.list[index];

        this.PaletteSolidBrush.Color=    this.PaletteAcrylicBrush.TintColor = this.PaletteAcrylicBrush.FallbackColor = await FanKit.Frames.Library.Palette.GetPaletteFormImage(uri);
        }



    }
}
