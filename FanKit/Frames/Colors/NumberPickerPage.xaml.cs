using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace FanKit.Frames.Colors
{
    public sealed partial class NumberPickerPage : Page
    {
        public NumberPickerPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/NumberPickerPage.xaml.txt");
                this.MarkdownText2.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/NumberPicker.xaml.txt");
                this.MarkdownText3.Text = await FanKit.Sample.File.GetFile("ms-appx:///TXT/Colors/NumberPicker.cs.txt");
            };
        }
        

        private void Slider0_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) => this.NumberPicker0.Value = (int)e.NewValue;
        private void Slider1_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) => this.NumberPicker1.Minimum = (int)e.NewValue;
        private void Slider2_ValueChanged(object sender, RangeBaseValueChangedEventArgs e) => this.NumberPicker2.Maximum = (int)e.NewValue;
        private void TextBox0_TextChanged(object sender, TextChangedEventArgs e) => this.NumberPicker3.Unit=   this.TextBox0.Text;

    }
}
