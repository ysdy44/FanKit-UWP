using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;


namespace FanKit.Library.Control
{
    public sealed partial class TabButton : UserControl
    {
        

        #region DependencyProperty


        public int SeletedIndex
        {
            get { return (int)GetValue(SeletedIndexProperty); }
            set { SetValue(SeletedIndexProperty, value); }
        }
        public static readonly DependencyProperty SeletedIndexProperty =
            DependencyProperty.Register("SeletedIndex", typeof(int), typeof(TabButton), new PropertyMetadata(0, new PropertyChangedCallback(SeletedIndexOnChanged)));
        private static void SeletedIndexOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TabButton Con = (TabButton)sender;

            int NewValue=(int) e.NewValue ;

            if (NewValue == Con.TabIndex)
                Con.Button.Foreground = Con.Resources["SeletedForeground"] as SolidColorBrush;
            else
                Con.Button.Foreground = Con.UsualForeground;
        }


        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
        DependencyProperty.Register("Icon", typeof(object), typeof(TabButton), new PropertyMetadata(null, new PropertyChangedCallback(IconOnChanged)));
        private static void IconOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TabButton Con = (TabButton)sender;

            Con.IconControl.Content = e.NewValue;
        }



        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(TabButton), new PropertyMetadata(string.Empty, new PropertyChangedCallback(TextOnChanged)));
        private static void TextOnChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TabButton Con = (TabButton)sender;

            Con.TextControl.Text = e.NewValue as string;
        }


        #endregion

        
        public TabButton()
        {
            this.InitializeComponent();
        }

        
        public bool IsHorizontal
        {
            get => this.StackPanel.Orientation == Orientation.Horizontal;
            set => this.StackPanel.Orientation = value ? Orientation.Horizontal : Orientation.Vertical;
        }
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e) => this.IsHorizontal = e.NewSize.Width > 100;


        private void Button_Tapped(object sender, TappedRoutedEventArgs e)=>    this.SeletedIndex = this.TabIndex;

    }
}
