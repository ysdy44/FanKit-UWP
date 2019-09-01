using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Samples
{
    /// <summary>
    /// Control of <see cref="FanKit.Samples.Sample"/>.
    /// </summary>
    public sealed partial class SampleControl : UserControl
    {
        //@Content
        public void SetSample(Sample sample)
        {
            if (sample == null) return;

            SampleState state = sample.State;
            this.FlagContentPresenter.Background = (state == SampleState.Disable) ? this.UnAccentColor : this.AccentColor;
            this.FlagContentPresenter.Foreground = (state == SampleState.Disable) ? this.UnCheckColor : this.CheckColor;
            this.FlagContentPresenter.Visibility = (state == SampleState.None) ? Visibility.Collapsed : Visibility.Visible;
            this.FlagContentPresenter.Content = (state == SampleState.None) ? string.Empty : state.ToString();

            Uri uri = sample.Uri;
            this.Image.Source = uri;

            string name = sample.Name;
            this.NameTextBlock.Text = name;
        }

        //@Construct
        public SampleControl(Sample sample)
        {            
            this.InitializeComponent();
            this.SetSample(sample);

            this.Image.SizeChanged += (s, e) =>
            {
                if (e.PreviousSize == e.NewSize) return;

                this.BackgroundRectangle.Width = e.NewSize.Width;
                this.BackgroundRectangle.Height = e.NewSize.Height;
            };

            this.Button.Tapped += (s, e) =>
            {
                Sample.FlyoutSample_Invoke(this, sample);//Delegate
                e.Handled = true;
            };

            this.RootGrid.PointerEntered += (s, e) => this.Entered();
            this.RootGrid.PointerExited += (s, e) => this.Exited();
            this.RootGrid.PointerPressed += (s, e) => this.Exited();
            this.RootGrid.PointerReleased += (s, e) => this.Exited();
            this.RootGrid.PointerCanceled += (s, e) => this.Exited();

            this.RootGrid.Tapped += (s, e) =>
            {
                if (sample == null) return;
                if (sample.State == SampleState.Disable) return;

                Type page = sample.Page;
                Sample.NavigatePage_Invoke(this, page);//Delegate
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