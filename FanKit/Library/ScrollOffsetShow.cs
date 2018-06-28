using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace FanKit.Library
{
    public  class ScrollOffsetShow : INotifyPropertyChanged
    {

        //delegate
        public delegate void ShowChangedHandler(bool IsShow);
        public event ShowChangedHandler ShowChanged = null;

        private Visibility upVisibility = Visibility.Visible;
        public Visibility UpVisibility
        {
            get => upVisibility;
            set
            {
                upVisibility = value;
                OnPropertyChanged("UpVisibility");
            }
        }

        private Visibility downVisibility = Visibility.Collapsed;
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
        private double Span = 0;

        private bool isShow;
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

        private double verticalOffset;
        private double VerticalOffset
        {
            get => verticalOffset;
            set
            {
                if (this.IsShow == false && value > verticalOffset)//在面板没有展示且向上滑动时
                    Span += verticalOffset - value; //累加向上滑动的距离
                else if (this.IsShow == true && value < verticalOffset) //在面板展示且向下滑动时
                    Span += verticalOffset - value;//累加向下滑动的距离

                //上滑，达到一定距离就不展示
                if (Span > 20) this.IsShow = false;
                //下滑，达到一定距离就展示
                else if (Span < -20) this.IsShow = true;

                verticalOffset = value;
            }
        }



        //Methon
        public void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e) => this.VerticalOffset = ((ScrollViewer)sender).VerticalOffset;



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
