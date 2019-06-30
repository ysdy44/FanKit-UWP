using System.Numerics;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Transformers
{
    /// <summary>
    /// Page of <see cref="FanKit.Transformers.RemoteControl">.
    /// </summary>
    public sealed partial class RemoteControlPage : Page
    {
        Vector2 v;
        private Vector2 vector;
        public Vector2 Vector
        {
            get => this.vector;
            set
            {
                this.RunX.Text = ((int)value.X).ToString();
                this.RunY.Text = ((int)value.Y).ToString();

                Canvas.SetLeft(this.Ellipse,value.X);
                Canvas.SetTop(this.Ellipse, value.Y);

                this.vector = value;
            }
        }

        //@Construct
        public RemoteControlPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Transformers/RemoteControlPage.xaml.txt");
            };

            this.Vector = Vector2.Zero;

            this.RemoteControl.Moved += (s, value) => this.Vector += value;
            this.RemoteControl.ValueChangeStarted += (s, value) => this.v = this.Vector;
            this.RemoteControl.ValueChangeDelta += (s, value) => this.Vector = this.v + value;
            this.RemoteControl.ValueChangeCompleted += (s, value) => { };
        }
    }
}