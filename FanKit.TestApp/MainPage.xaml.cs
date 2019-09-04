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
using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI;
using Windows.Storage;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using System.Linq;

namespace FanKit.TestApp
{
    public class SamplesCategory
    {
        public string Name { get; set; }
        public List<ContentControl> Items { get; set; }
    }

    public sealed partial class MainPage : Page
    {
        public bool IsShowFlyout
        {
            set
            {
                this.FlyoutCanvas.IsHitTestVisible = value;
                this.FlyoutContentControl.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public bool IsExpand
        {
            get => this.GridViewBorder.Visibility == Visibility.Visible;
            set
            {
                this.GridViewBorder.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private string categoryName;
        public string CategoryName
        {
            get => this.categoryName;
            set
            {
                this.FlyoutContentControl.Content = value;
                this.categoryName = value;
            }
        }

        List<SamplesCategory> _samplesCategory;

        public MainPage()
        {
            this.InitializeComponent();

            ContentControl getSample(string name) => new ContentControl
            {
                Content = name,
                Template = this.GridViewControlTemplate
            };
            this._samplesCategory = new List<SamplesCategory>
            {
                new SamplesCategory{ Name = "AAA", Items = new List<ContentControl>{ getSample("Red"), getSample("Orange"), getSample("Yellow"), getSample("Green"), getSample("Blue"), getSample("Purple"), getSample("Black and white"), getSample("Rainbow color"), getSample("Colorful")} },
                new SamplesCategory{ Name = "BBB", Items = new List<ContentControl>{  getSample("Orange"), getSample("Green"), getSample("Red"), getSample("Green"), getSample("Purple"), getSample("Blue"), getSample("Black and white"), getSample("Rainbow color"), getSample("Colorful")} },
                new SamplesCategory{ Name = "CCC", Items = new List<ContentControl>{ getSample("Blue"), getSample("Yellow"), getSample("Green"), getSample("Blue"), getSample("Red"), getSample("Orange"), getSample("Black and white"), getSample("Rainbow color"), getSample("Colorful") }},
                new SamplesCategory{ Name = "DDD", Items = new List<ContentControl>{  getSample("Green"), getSample("Orange"), getSample("Yellow"), getSample("Red"), getSample("Blue"), getSample("Purple"), getSample("Black and white"), getSample("Rainbow color"), getSample("Colorful") }},
                new SamplesCategory{ Name = "EEE", Items = new List<ContentControl>{ getSample("Red"), getSample("Orange"), getSample("Yellow"), getSample("Green"), getSample("Blue"), getSample("Purple"), getSample("Black and white"), getSample("Rainbow color"), getSample("Colorful")} },
                new SamplesCategory{ Name = "FFF", Items = new List<ContentControl>{  getSample("Orange"), getSample("Green"), getSample("Red"), getSample("Green"), getSample("Purple"), getSample("Blue"), getSample("Black and white"), getSample("Rainbow color"), getSample("Colorful")} },
            };

            this.IsExpand = false;
            this.IsShowFlyout = false;
            this.FlyoutCanvas.Tapped += (s, e) => this.IsShowFlyout = false;

            this.ListView.ItemsSource = this._samplesCategory;
            this.ListView.IsItemClickEnabled = true;
            this.ListView.ItemClick += (s, e) =>
            {
                if (e.ClickedItem is SamplesCategory samplesCategory)
                {
                    this.SetSampleCategory(samplesCategory);
                }
            };
        }

        private void Button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Parent is Grid grid)
                {
                    if (grid.Parent is Grid rootGrid)
                    {
                        this.IsShowFlyout = true;
                        this.SetCoords(rootGrid);
                    }
                }
            }
        }

        public void SetCoords(FrameworkElement element)
        {
            GeneralTransform transform = element.TransformToVisual(Window.Current.Content);

            Point screenCoords = transform.TransformPoint(new Point(0, 0));
            Point centerCoords = new Point(screenCoords.X + element.ActualWidth / 2, screenCoords.Y + element.ActualHeight / 2);

            double actualWidth = this.FlyoutContentControl.ActualWidth < 50 ? 200 : this.FlyoutContentControl.ActualWidth;
            double actualHeight = this.FlyoutContentControl.ActualHeight < 50 ? 240 : this.FlyoutContentControl.ActualHeight;

            double x = centerCoords.X - actualWidth / 2;
            double y = centerCoords.Y - actualHeight / 2;

            Canvas.SetLeft(this.FlyoutContentControl, x < 0 ? 0 : x);
            Canvas.SetTop(this.FlyoutContentControl, y < 0 ? 0 : y);
        }

        public void SetSampleCategory(SamplesCategory samplesCategory)
        {
            if (this.IsExpand && this.CategoryName == samplesCategory.Name)
            {
                this.IsExpand = false;
            }
            else
            {
                this.GridView.Children.Clear();
                this.IsExpand = true;

                foreach (ContentControl contentControl in samplesCategory.Items)
                {
                    this.GridView.Children.Add(contentControl);
                }
            }

            this.CategoryName = samplesCategory.Name;
        }
    }
}