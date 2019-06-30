using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Samples
{
    /// <summary>
    /// The <see cref="ScrollViewer.VerticalOffsetProperty"> changes the <see cref="Button.VisibilityProperty">.
    /// </summary>
    public class ScrollOffsetShow : INotifyPropertyChanged
    {
        //delegate
        public delegate void ShowChangedHandler(bool IsShow);
        public event ShowChangedHandler ShowChanged = null;
        

        /// <summary> Up </summary>
        public Visibility UpVisibility
        {
            get => this.upVisibility;
            set
            {
                this.upVisibility = value;
                OnPropertyChanged(nameof(this.UpVisibility));//Notify
            }
        }
        private Visibility upVisibility = Visibility.Visible;
        
        /// <summary> Down </summary>
        public Visibility DownVisibility
        {
            get => this.downVisibility;
            set
            {
                this.downVisibility = value;
                OnPropertyChanged(nameof(this.DownVisibility));//Notify
            }
        }
        private Visibility downVisibility = Visibility.Collapsed;

        
        //Property   
        private double Span = 0;//cache

        private bool isShow; //main
        public bool IsShow
        {
            get => isShow;
            set
            {
                if (value != isShow)
                {
                    this.ShowChanged?.Invoke(value);

                    if (value)
                    {
                        this.UpVisibility = Visibility.Collapsed;
                        this.DownVisibility = Visibility.Visible;
                    }
                    else
                    {
                        this.UpVisibility = Visibility.Visible;
                        this.DownVisibility = Visibility.Collapsed;
                    }

                    isShow = value;
                }
            }
        }
        
        private double verticalOffset;//offset
        public double VerticalOffset
        {
            get => verticalOffset;
            set
            {
                if (this.IsShow == false && value > verticalOffset)
                    Span += verticalOffset - value; //Down: cache offset
                else if (this.IsShow == true && value < verticalOffset)
                    Span += verticalOffset - value;//Up: cache offset

                //Up: overflow
                if (Span > 20) this.IsShow = false;
                //Down: overflow
                else if (Span < -20) this.IsShow = true;

                verticalOffset = value;
            }
        }
                

        public void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e) => this.VerticalOffset = ((ScrollViewer)sender).VerticalOffset;


        //Notify
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
