using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;

namespace FanKit.Frames.Library
{

    public class DetailsViewService
    { 
        private SplitFrame Frame { set; get; }


          public void InitializeComponent(SplitFrame frame, Type welcomePage)
        {
            this.Frame = frame;
            this.Frame.Navigate(welcomePage);

            //返回按钮不可视
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            //返回事件注册
            SystemNavigationManager.GetForCurrentView().BackRequested += (sender, e) =>
            {
                if (this.Frame.CanGoBack && e.Handled == false)
                {
                    e.Handled = true;
                    this.Frame.GoBack();
                }
            };

            //页面跳转结束事件
            this.Frame.Navigated += (sender, e) =>
            {
                if (this.Frame.CanGoBack || this.Frame.IsSplit)
                    this.Frame.Visibility = Visibility.Visible;
                else
                    this.Frame.Visibility = Visibility.Collapsed;
            };

            //页面跳转结束事件
            this.Frame.DetailChanged += () =>
            {
                if (this.Frame.CanGoBack || this.Frame.IsSplit)
                    this.Frame.Visibility = Visibility.Visible;
                else
                    this.Frame.Visibility = Visibility.Collapsed;
            };
        }


        #region Navigate

         
        public void Navigate(Type sourcePageType, object parameter = null)
        {
            if (this.Frame == null) return;

            //跳转
            this.Frame.Navigate(sourcePageType, parameter);
        }

        
        public void ReNavigate(Type sourcePageType, object parameter = null)
        {
            if (this.Frame == null) return;

            while (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }

            this.Frame.Navigate(sourcePageType, parameter);
        }
        
        public void GoBack()
        {
            if (this.Frame == null) return;
            if (this.Frame.CanGoBack == false) return;

            this.Frame.GoBack();
        }


        #endregion

    }

    public class SplitFrame : Frame
    {

        public delegate void DetailChangedEventHandler();
        public event DetailChangedEventHandler DetailChanged;

        #region DependencyProperty


        public bool IsSplit
        {
            get { return (bool)GetValue(IsSplitProperty); }
            set { SetValue(IsSplitProperty, value); }
        }
        public static readonly DependencyProperty IsSplitProperty = DependencyProperty.Register("IsSplit", typeof(bool), typeof(SplitFrame), new PropertyMetadata(false, OnIsSplitChanged));
        private static void OnIsSplitChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is SplitFrame con && e.NewValue != e.OldValue)
            {
                con.DetailChanged?.Invoke();
            }
        }

        #endregion

    }

}
