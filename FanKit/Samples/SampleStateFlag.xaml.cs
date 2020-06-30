using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FanKit.Samples
{
    /// <summary>
    /// Flag of <see cref="SampleState"/>.
    /// </summary>
    public sealed partial class SampleStateFlag : UserControl
    {

        //@VisualState
        SampleState _vsState = SampleState.None;
        /// <summary> 
        /// Represents the visual appearance of UI elements in a specific state.
        /// </summary>
        public VisualState VisualState
        {
            get
            {
                switch (this._vsState)
                {
                    case SampleState.None: return this.Normal;
                    case SampleState.New: return this.New;
                    case SampleState.Updated: return this.Updated;
                    case SampleState.Preview: return this.Preview;
                    case SampleState.Recom: return this.Recom;
                    case SampleState.Disable: return this.Disable;
                    default: return this.Normal;
                }
            }
            set => VisualStateManager.GoToState(this, value.Name, false);
        }


        #region DependencyProperty


        /// <summary> Gets or sets the state. </summary>
        public SampleState State
        {
            get { return (SampleState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        /// <summary> Identifies the <see cref = "SampleStateFlag.State" /> dependency property. </summary>
        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof(State), typeof(SampleState), typeof(SampleStateFlag), new PropertyMetadata(SampleState.None, (sender, e) =>
        {
            SampleStateFlag con = (SampleStateFlag)sender;

            if (e.NewValue is SampleState value)
            {
                con._vsState = value;
                con.VisualState = con.VisualState;//State
            }
        }));


        #endregion


        //@Construct
        /// <summary>
        /// Initializes a StateComboBox. 
        /// </summary>
        public SampleStateFlag()
        {
            this.InitializeComponent();
        }
    }
}
