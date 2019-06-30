using System;
using System.ComponentModel;
using Windows.UI.Xaml.Input;

namespace FanKit.Samples
{
    /// <summary>
    /// Simple sample item.
    /// </summary>
    public class Sample : INotifyPropertyChanged
    {
        /// <summary> Sample's state. </summary>
        public SampleState State
        {
            get => this.state;
            set
            {
                this.state = value;
                this.OnPropertyChanged(nameof(this.State));//Notify
            }
        }
        private SampleState state;

        /// <summary> Sample's frame page. </summary>
        public Type Page
        {
            get => this.page;
            set
            {
                this.page = value;
                this.OnPropertyChanged(nameof(this.Page));//Notify
            }
        }
        private Type page;

        /// <summary> Sample's name. </summary>
        public string Name
        {
            get => this.name;
            set
            {
                this.name = value;
                this.OnPropertyChanged(nameof(this.Name));//Notify
            }
        }
        private string name;

        /// <summary> Sample's image uri. </summary>
        public Uri Uri
        {
            get => this.uri;
            set
            {
                this.uri = value;
                this.OnPropertyChanged(nameof(this.Uri));//Notify
            }
        }
        private Uri uri;

        /// <summary> Sample's summary. </summary>
        public string Summary
        {
            get => this.summary;
            set
            {
                this.summary = value;
                this.OnPropertyChanged(nameof(this.Summary));//Notify
            }
        }
        private string summary;


        public void Button_Tapped(object sender, TappedRoutedEventArgs e)=> e.Handled = true;

        //Notify
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
