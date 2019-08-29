using FanKit.Samples;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit
{
    /// <summary>
    /// Control of <see cref="FanKit.Samples. SampleCategory"/>.
    /// </summary>
    public sealed partial class SamplesCategoryControl : UserControl
    {
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
                        this.GridView.Children.Clear();
                        foreach (Sample sample in value.Samples)
                        {
                            this.GridView.Children.Add(sample.Instance);
                        }
                    }
                }

                this.sampleCategory = value;
            }
        }
        private SampleCategory sampleCategory;
        
        //@Construct
        public SamplesCategoryControl()
        {
            this.InitializeComponent();
            this.Visibility = Visibility.Collapsed;
            this.BlurBorder.Tapped += (s, e) => this.Visibility = Visibility.Collapsed;
        }
    }
}