using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml.Controls;

namespace FanKit.Control
{
    public sealed partial class PopupMenu : UserControl
    {
         
        /// <summary> Show or hide. </summary>
        public bool IsShow
        {
            get=> this.isShow;
            set
            {
                if (value) this.Flyout.ShowAt(this);
                else this.Flyout.Hide();

                this.isShow = value;
            }
        }
        private bool isShow;

        /// <summary> Coped. </summary>
        public string Text { get; set; }
        

        public PopupMenu()
        {
            this.InitializeComponent();
            this.Holding += (s, e) => this.IsShow = true;//Flyout
            this.DoubleTapped += (s, e) => this.IsShow = true;//Flyout
            this.RightTapped += (s, e) => this.IsShow = true;//Flyout

            this.Button0.Tapped += (s, e) => this.Copy();
            this.Button1.Tapped += (s, e) => this.Copy();
            this.Button2.Tapped += (s, e) => this.Copy();
            this.Button3.Tapped += (s, e) => this.Copy();
            this.Button4.Tapped += (s, e) => this.Copy();
            this.Button5.Tapped += (s, e) => this.Copy();
            this.Button6.Tapped += (s, e) => this.Copy();
        }

        private void Copy()
        {
            //Clipboard
            DataPackage dataPackage = new DataPackage();
            dataPackage.SetText(this.Text);
            Clipboard.SetContent(dataPackage);

            this.IsShow = false;//Flyout
        }
    }
}
