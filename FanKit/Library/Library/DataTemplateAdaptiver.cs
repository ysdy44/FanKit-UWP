using System;
using System.ComponentModel;
using Windows.UI.Xaml;

namespace FanKit.Library.Library
{
    public class DataTemplateAdaptiver : DependencyObject, INotifyPropertyChanged
    {

        #region DependencyProperty


        //Width
        public double sizeWidth;
        public double SizeWidth
        {
            get => sizeWidth; 
            set
            {
                sizeWidth = value;
                OnPropertyChanged(nameof(SizeWidth));
            }
        }
        //Height
        public double sizeHeight;
        public double SizeHeight
        {
            get => sizeHeight;
            set
            {
                sizeHeight = value;
                OnPropertyChanged(nameof(SizeHeight));
            }
        }

        //DependencyProperty
        public FrameworkElement PanelElement
        {
            get { return (FrameworkElement)GetValue(PanelElementProperty); }
            set { SetValue(PanelElementProperty, value); }
        }
        public static DependencyProperty PanelElementProperty = DependencyProperty.Register(nameof(PanelElement), typeof(FrameworkElement), typeof(DataTemplateAdaptiver), new PropertyMetadata(null, OnPanelElementChanged));
        private static void OnPanelElementChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is DataTemplateAdaptiver con)
            {
                if (e.NewValue != e.OldValue)
                {
                    con.PanelElement.SizeChanged += (sender2, e2) =>
                    {
                        if (e2.NewSize.Width > 100 && e2.NewSize.Height > 100)
                            con.OnSizeChanged(e2);
                     };
                }
            }
        }


        #endregion


        protected void OnSizeChanged(SizeChangedEventArgs e)
        {
            double width = this.GetSizeWidth(e.NewSize.Width);

            this.SizeWidth = width;
            this.SizeHeight = width * 1.6d;
        }
        private double GetSizeWidth(double width)
        {
            double designWidth = Math.Sqrt(Math.Sqrt(width)) * 24d;//design Width

            int count = ((int)(width / designWidth));//count of transverse

            return (width - 4) / count;//Aliquot width
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}









