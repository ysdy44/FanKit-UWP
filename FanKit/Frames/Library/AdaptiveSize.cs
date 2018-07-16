using System.ComponentModel;
using Windows.UI.Xaml;


namespace FanKit.Frames.Library
{
    public class AdaptiveSize : DependencyObject, INotifyPropertyChanged
    {

        #region DependencyProperty


        //DependencyProperty
        public FrameworkElement PanelElement
        {
            get { return (FrameworkElement)GetValue(PanelElementProperty); }
            set { SetValue(PanelElementProperty, value); }
        }
        public static DependencyProperty PanelElementProperty = DependencyProperty.Register(nameof(PanelElement), typeof(FrameworkElement), typeof(AdaptiveSize), new PropertyMetadata(null, OnPanelElementChanged));
        private static void OnPanelElementChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is AdaptiveSize con && e.NewValue != e.OldValue)
            {
                con.PanelElement.SizeChanged += (s2, e2) => con.SetSizeWidth();
                con.SetSizeWidth();
            }
        }


        //Design Width
        public double DesignWidth
        {
            get { return (double)GetValue(DesignWidthProperty); }
            set { SetValue(DesignWidthProperty, value); }
        }
        public static readonly DependencyProperty DesignWidthProperty = DependencyProperty.Register("DesignWidth", typeof(double), typeof(AdaptiveSize), new PropertyMetadata(120d));


        #endregion



        //Size Width
        public double sizeWidth;
        public double SizeWidth
        {
            get => sizeWidth; 
            set
            {
                sizeWidth = value;
                OnPropertyChanged("SizeWidth");
            }
        }

        public double sizeHeight;
        public double SizeHeight
        {
            get => sizeHeight;
            set
            {
                sizeHeight = value;
                OnPropertyChanged("SizeHeight");
            }
        }



        //Set/Get Width  
        private void SetSizeWidth()
        {
            double width = this.GetSizeWidth();
            this.SizeWidth = width;
            this.SizeHeight = width*1.6d;
        }
        private double GetSizeWidth()
        {
            double width = this.PanelElement.ActualWidth;
            if (this.PanelElement != null && width > 100)
            {
                int count = ((int)(width / DesignWidth));//count of transverse
                return (PanelElement.ActualWidth - 4) / count;//Aliquot width
            }
            else
                return DesignWidth;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)=>  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
