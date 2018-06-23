using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.ApplicationModel.DataTransfer;

namespace FanKit.Frames.Control
{
    public sealed partial class PopupMenuControl : UserControl
    {

        #region DependencyProperty


        /// <summary>
        /// show or hide
        /// </summary>
        private bool isShow;
        public bool IsShow
        {
            get
            {
                return this.isShow;
            }
            set
            {
                if (value) this.Flyout.ShowAt(this);
                else this.Flyout.Hide();

                this.isShow = value;
            }
        }

        /// <summary>
        /// copy the text
        /// </summary>
        private string text;
        public string Text
        {
            get => this.text;
            set => this.text = value;
        }
        
        #endregion


        public PopupMenuControl()
        {
            this.InitializeComponent();
        }


        private void UserControl_Holding(object sender, HoldingRoutedEventArgs e) => this.IsShow = true;//Flyout
        private void UserControl_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e) => this.IsShow = true;//Flyout
        private void UserControl_RightTapped(object sender, RightTappedRoutedEventArgs e) => this.IsShow = true;//Flyout


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Clipboard
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(this.Text);
            Clipboard.SetContent(dataPackage);

            this.IsShow = false;//Flyout
        }

    }
}
