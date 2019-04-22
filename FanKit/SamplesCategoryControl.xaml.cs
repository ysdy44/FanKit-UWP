using FanKit.Samples;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit
{
    public sealed partial class SamplesCategoryControl : UserControl
    {
        #region DependencyProperty


        public SampleCategory Category
        {
            get { return (SampleCategory)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register(nameof(Category), typeof(SampleCategory), typeof(SamplesCategoryControl), new PropertyMetadata(null, (sender, e) =>
        {
            SamplesCategoryControl con = sender as SamplesCategoryControl;

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
        }));


        #endregion
        
        //Delegate
        public delegate void ItemClickHandler(Type page);
        public event ItemClickHandler ItemClick = null;
        
        public SamplesCategoryControl()
        {
            this.InitializeComponent();
            this.ShadowGrid.Tapped += (s, e) => this.Category = null;

            this.GridView.IsItemClickEnabled = true;
            this.GridView.ItemClick += (s, e) =>
             {
                 if (e.ClickedItem is Sample sample)
                 {
                     Type page = sample.Page;
                     this.ItemClick?.Invoke(page);//Delegate
                }
             };          
        }
    }
}
