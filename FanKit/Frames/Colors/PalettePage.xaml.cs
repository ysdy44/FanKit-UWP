using System;
using System.Collections.Generic;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Colors
{
    /// <summary>
    /// Page of <see cref="HSVColorPickers.Palette">.
    /// </summary>
    public sealed partial class PalettePage : Page
    {

        List<Uri> ItemsSource = new List<Uri>
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
            this.Loaded += async (s2, e2) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/PalettePage.xaml.txt");
                this.MarkdownText1.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Colors/PalettePage.xaml"));
                this.MarkdownText2.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Colors/PalettePage.xaml.cs.txt");
                this.MarkdownText2.LinkClicked += async (s, e) => await Launcher.LaunchUriAsync(new Uri("https://github.com/ysdy44/FanKit-UWP/blob/master/FanKit/Frames/Colors/PalettePage.xaml.cs"));
            };
            this.CarouselControl.ItemsSource = this.ItemsSource;
            this.CarouselControl.SelectionChanged += async (s, e) =>
            {
                //Uri
                int index = this.CarouselControl.SelectedIndex;
                Uri uri = this.ItemsSource[index];

                //Color
                Color color = await HSVColorPickers.Palette.GetColorFormImage(uri);
                this.PaletteSolidBrush.Color = this.PaletteAcrylicBrush.TintColor = this.PaletteAcrylicBrush.FallbackColor = color;
            };
        }
    }
}