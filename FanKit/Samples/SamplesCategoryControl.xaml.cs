using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Samples
{
    /// <summary>
    /// Control of <see cref="FanKit.Samples.SamplesCategory"/>.
    /// </summary>
    public sealed partial class SamplesCategoryControl : UserControl
    {
        /// <summary> Category name </summary>
        public string CategoryName;

        /// <summary> is expand? </summary>
        public bool IsExpand
        {
            get => this.DropShadowPanel.Visibility == Visibility.Visible;
            set
            {
                Visibility visibility = value ? Visibility.Visible : Visibility.Collapsed;

                this.DropShadowPanel.Visibility = visibility;
                this.BlurBorder.Visibility = visibility;
            }
        }
        
        //@Construct
        public SamplesCategoryControl()
        {
            this.InitializeComponent();
            this.IsExpand = false;
            this.BlurBorder.Tapped += (s, e) => this.IsExpand = false;
        }

        /// <summary>
        /// Sets SampleCategory for this.
        /// </summary>
        /// <param name="sampleCategory"> SampleCategory </param>
        public void SetSampleCategory(SamplesCategory sampleCategory)
        {
            if (this.IsExpand && this.CategoryName == sampleCategory.Name)
            {
                this.IsExpand = false;
            }
            else
            {
                this.GridView.Children.Clear();
                this.IsExpand = true;

                foreach (Sample sample in sampleCategory.Samples)
                {
                    this.GridView.Children.Add(sample.Instance);
                }
            }

            this.CategoryName = sampleCategory.Name;
        }
    }
}