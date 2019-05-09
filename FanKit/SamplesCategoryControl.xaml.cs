using FanKit.Samples;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit
{
    public sealed partial class SamplesCategoryControl : UserControl
    {
        //Delegate
        public delegate void ItemClickHandler(Type page);
        public event ItemClickHandler ItemClick = null;

        #region DependencyProperty

        /// <summary> ItemsSource </summary>
        public SampleCategory SampleCategory
        {
            get => this.sampleCategory;
            set
            {
                if (value != null)
                {
                    if (this.sampleCategory != value)
                    {
                        this.GridView.ItemsSource = value.Samples;
                    }
                }

                this.sampleCategory = value;
            }
        }
        private SampleCategory sampleCategory;
         

        #endregion

        public SamplesCategoryControl()
        {
            this.InitializeComponent();
            this.Visibility = Visibility.Collapsed;
            this.BlurBorder.Tapped += (s, e) => this.Visibility = Visibility.Collapsed;

            this.GridView.IsItemClickEnabled = true;
            this.GridView.ItemClick += (s, e) =>
            {                 
                 if (e.ClickedItem is Sample sample)
                 {
                     switch (sample.State)
                     {
                         case SampleState.Disable:
                            return;                            
                     }
                    this.Visibility = Visibility.Collapsed;

                    Type page = sample.Page;
                    this.ItemClick?.Invoke(page);//Delegate
                 }
            };          
        }
    }
}
