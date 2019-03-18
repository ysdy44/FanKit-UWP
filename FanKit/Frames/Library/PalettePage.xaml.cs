using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

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
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Library/Palette.xaml.cs.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Library/Palette.cs.txt");
            };
        }


        private async void CarouselControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = this.CarouselControl.SelectedIndex;
            Uri uri = this.list[index];

            var color = await FanKit.Library.Palette.GetPaletteFormImage(uri);
            this.PaletteSolidBrush.Color = this.PaletteAcrylicBrush.TintColor = this.PaletteAcrylicBrush.FallbackColor = color;
        }



    }
}





