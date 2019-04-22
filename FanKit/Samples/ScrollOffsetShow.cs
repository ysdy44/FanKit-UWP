using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace FanKit.Samples
{
    public  class ScrollOffsetShow : INotifyPropertyChanged
    {

        //delegate
        public delegate void ShowChangedHandler(bool IsShow);
        public event ShowChangedHandler ShowChanged = null;

        private Visibility upVisibility = Visibility.Visible;//上划可视
        public Visibility UpVisibility
        {
            get => upVisibility;
            set
            {
                upVisibility = value;
                OnPropertyChanged("UpVisibility");
            }
        }

        private Visibility downVisibility = Visibility.Collapsed;//下滑可视
        public Visibility DownVisibility
        {
            get => downVisibility;
            set
            {
                downVisibility = value;
                OnPropertyChanged("DownVisibility");
            }
        }




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




        //Methon
        public void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e) => this.VerticalOffset = ((ScrollViewer)sender).VerticalOffset;



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
