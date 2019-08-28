using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace FanKit.Frames.Styles
{
    public sealed partial class ChildrenTransitionPage : Page
    {

        int _index;
        ObservableCollection<string> _list { get; } = new ObservableCollection<string>();

        public ChildrenTransitionPage()
        {
            this.InitializeComponent();
            this.Loaded += async (sender, e) =>
            {
                this.ListBox.SelectedIndex = 0;
                this.CountComboBox.SelectedIndex = 2;
                this.MarkdownText1.Text = await FanKit.Samples.File.GetFile("ms-appx:///TXT/Styles/ChildrenTransition.style.txt");  
            };

            this.ListBox.SelectionChanged += (s, e) =>
            {
                int index = this.ListBox.SelectedIndex;
                Transition transition = this.GetTransition(index);

                if (transition == null) this.StackPanel.ChildrenTransitions = null;
                else this.StackPanel.ChildrenTransitions = new TransitionCollection
                {
                     transition
                };
            };

            this.AddButton.Tapped += (sender, e) =>
            {
                Style style = this.Resources["ContentControlStyle"] as Style;

                for (int i = this.CountComboBox.SelectedIndex; i >= 0; i--)
                {
                    this._index++;
                    string content = this._index.ToString();
                    if (content.Length == 1) content = "00" + content;
                    else if (content.Length == 2) content = "0" + content;

                    ContentControl contentControl = new ContentControl()
                    {
                        Content = content,
                        Style = style,
                    };

                    this.StackPanel.Children.Add(contentControl);
                }
            };
            this.RemoveButton.Tapped += (sender, e) =>
            {
                for (int i = this.CountComboBox.SelectedIndex; i >= 0; i--)
                {
                    UIElement uIElement = this.StackPanel.Children.LastOrDefault();
                    if (uIElement != null)
                    {
                        this.StackPanel.Children.Remove(uIElement);
                    }
                }
            };
        }

        private Transition GetTransition(int index)
        {
            switch (index)
            {
                case 0: return new EntranceThemeTransition()
                {
                    IsStaggeringEnabled = true
                };
                case 1: return new ContentThemeTransition();
                case 2: return new PopupThemeTransition();
                case 3: return new AddDeleteThemeTransition();
                case 4: return new ReorderThemeTransition();
                case 5: return new PaneThemeTransition();
                case 6: return new EdgeUIThemeTransition();
                default: return null;
            }
        }
    }
}