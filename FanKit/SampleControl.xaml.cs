using FanKit.Samples;
using Microsoft.Toolkit.Uwp.UI.Animations;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit
{
    /// <summary>
    /// Control of <see cref="FanKit.Samples.Sample"/>.
    /// </summary>
    public sealed partial class SampleControl : UserControl
    {
        //@Content
        private void SetSample(Sample sample)
        {
            if (sample == null) return;

            SampleState state = sample.State;
            this.FlagContentPresenter.Background = (state == SampleState.Disable) ? this.UnAccentColor : this.AccentColor;
            this.FlagContentPresenter.Foreground = (state == SampleState.Disable) ? this.UnCheckColor : this.CheckColor;
            this.FlagContentPresenter.Visibility = (state == SampleState.None) ? Visibility.Collapsed : Visibility.Visible;
            this.FlagContentPresenter.Content = (state == SampleState.None) ? string.Empty : state.ToString();

            Uri uri = sample.Uri;
            this.ImageEx.Source = uri;
            this.FlyoutImageEx.Source = uri;

            string name = sample.Name;
            this.NameTextBlock.Text = name;
            this.FlyoutNameTextBlock.Text = name;

            string summary = sample.Summary;
            this.FlyoutSummaryTextBlock.Text = summary;
        }

        //@Construct
        public SampleControl(Sample sample)
        {            
            this.InitializeComponent();
            this.SetSample(sample);

            this.Button.Tapped += (s, e) => e.Handled = true;

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
            //Opacity
            //OpacityAnimation animation = new OpacityAnimation() { To = 1, Duration = TimeSpan.FromMilliseconds(500) };
            //animation.StartAnimation(this.DropShadowPanel);

            //Scale
            ScaleAnimation parentAnimation = new ScaleAnimation() { To = "1.1", Duration = TimeSpan.FromMilliseconds(600) };
            parentAnimation.StartAnimation(this.DropShadowPanel);
        }
        private void Exited()
        {
            //Opacity
            //OpacityAnimation animation = new OpacityAnimation() { To = 0, Duration = TimeSpan.FromMilliseconds(2000) };
            //animation.StartAnimation(this.DropShadowPanel);

            //Scale
            ScaleAnimation parentAnimation = new ScaleAnimation() { To = "1", Duration = TimeSpan.FromMilliseconds(2000) };
            parentAnimation.StartAnimation(this.DropShadowPanel);
        }
    }
}