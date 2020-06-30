using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;

namespace FanKit.Samples
{
    /// <summary>
    /// Simple sample item.
    /// </summary>
    public partial class Sample : UserControl
    {
        //@Delegate
        /// <summary> Occurs when an item in a list view receives an interaction. </summary>
        public static event TypedEventHandler<object, Type> ItemClick;
        /// <summary> <see cref="Sample.ItemClick"/> </summary>
        public static void ItemClick_Invoke(object sender, Type page) => Sample.ItemClick?.Invoke(sender, page);

        /// <summary> Occurs when the flyout button receives the interaction. </summary>
        public static event TypedEventHandler<object, Sample> FlyoutShow;

        //@Construct
        /// <summary>
        /// Initializes a Sample. 
        /// </summary>
        public Sample()
        {            
            this.InitializeComponent();

            this.Image.SizeChanged += (s, e) =>
            {
                if (e.PreviousSize == e.NewSize) return;

                this.BackgroundRectangle.Width = e.NewSize.Width;
                this.BackgroundRectangle.Height = e.NewSize.Height;
            };

            this.FlyoutButton.Tapped += (s, e) =>
            {
                UserControl userControl = this;
                Sample.FlyoutShow?.Invoke(userControl, this);//Delegate
                e.Handled = true;
            };

            this.RootGrid.PointerEntered += (s, e) => this.Entered();
            this.RootGrid.PointerExited += (s, e) => this.Exited();
            this.RootGrid.PointerPressed += (s, e) => this.Exited();
            this.RootGrid.PointerReleased += (s, e) => this.Exited();
            this.RootGrid.PointerCanceled += (s, e) => this.Exited();

            this.RootGrid.Tapped += (s, e) =>
            {
                Sample.ItemClick?.Invoke(this, this.Page);//Delegate
            };
        }
        
        private void Entered()
        {
            this.ShowStoryboard.Begin();
        }
        private void Exited()
        {
            this.FadeStoryboard.Begin();
        }
    }
}