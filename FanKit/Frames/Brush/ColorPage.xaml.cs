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
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FanKit.Frames.Brush
{ 
    public sealed partial class ColorPage : Page
    {
        public ColorPage()
        {
            this.InitializeComponent();


            this.Paragraph1.Text = "<Border>";
            this.Paragraph2.Text = "  <Border.Background>";

            this.TopRun1.Text = "    <SolidColorBrush  Color=\"{ ThemeResource ";
            this.TopRun2.Text = "SystemAltHighColor";
            this.TopRun3.Text = "}\" /> ";

            this.Paragraph4.Text = "  </Border.Background>";
            this.Paragraph5.Text = "</Border>";
        }


        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            double Offset = ((ScrollViewer)sender).VerticalOffset;

            //ImageButtonVisible：图片按钮可视
            FanKit.MainPage.ImageButtonVisibleChange(Offset);

            //顶栏
            this.TopGrid.Height = App.GetHeight(Offset);
        }

    }
}
