using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace FanKit.Samples
{
    public class Sample : INotifyPropertyChanged
    {
        private Type page;
        public Type Page
        {
            get => page;
            set
            {
                page = value;
                OnPropertyChanged(nameof(Page));
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private Uri uri;
        public Uri Uri
        {
            get => uri;
            set
            {
                uri = value;
                OnPropertyChanged(nameof(Uri));
            }
        }

        private string summary;
        public string Summary
        {
            get => summary;
            set
            {
                summary = value;
                OnPropertyChanged(nameof(Summary));
            }
        }
        

        public void Button_Tapped(object sender, TappedRoutedEventArgs e)=> e.Handled = true;


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
