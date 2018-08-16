using FanKit.Library;
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
using Microsoft.Toolkit.Uwp.UI.Animations;

namespace FanKit
{
    public sealed partial class SampleCategoryControl : UserControl
    {

        #region DependencyProperty


        public SampleCategory Category
        {
            get { return (SampleCategory)GetValue(SampleCategoryProperty); }
            set { SetValue(SampleCategoryProperty, value); }
        }
        public static readonly DependencyProperty SampleCategoryProperty =
            DependencyProperty.Register("SampleCategory", typeof(SampleCategory), typeof(SampleCategoryControl), new PropertyMetadata(null, new PropertyChangedCallback(SampleCategoryOnChanged)));

        public static void SampleCategoryOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            SampleCategoryControl con = sender as SampleCategoryControl;

            if (e.NewValue != null)
            {
                if (e.NewValue != e.OldValue)
                {
                    if (e.NewValue is SampleCategory category)
                    {
                        con.GridView.ItemsSource = category.Samples;
                        con.ShadowGrid.Visibility = Visibility.Visible;

                        return;
                    }
                }
            }

            con.ShadowGrid.Visibility = Visibility.Collapsed;
        }


        #endregion

        //delegate
        public delegate void ItemClickHandler(Type page);
        public event ItemClickHandler ItemClick = null;


        public SampleCategoryControl()
        {
            this.InitializeComponent();
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e) => this.ItemClick?.Invoke((e.ClickedItem as Sample).Page);
        private void ShadowGrid_Tapped(object sender, TappedRoutedEventArgs e) => this.Category = null;

     
    }
}
