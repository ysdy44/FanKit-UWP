using Newtonsoft.Json;
using System;
using Windows.Foundation;
using Windows.UI.Xaml;

namespace FanKit.Samples
{
    /// <summary>
    /// Simple sample item.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Sample 
    {
        //@Delegate
        /// <summary> Navigate page event. </summary>
        public static event TypedEventHandler<object, Type> NavigatePage;
        /// <summary> Navigate page method. </summary>
        public static void NavigatePage_Invoke(object sender, Type page) => Sample.NavigatePage?.Invoke(sender, page);


        /// <summary> Sample's state. </summary>
        [JsonProperty]
        public SampleState State;

        /// <summary> Sample's frame page. </summary>
        [JsonProperty]
        public Type Page;

        /// <summary> Sample's name. </summary>
        [JsonProperty]
        public string Name;

        /// <summary> Sample's image uri. </summary>
        [JsonProperty]
        public Uri Uri;

        /// <summary> Sample's summary. </summary>
        [JsonProperty]
        public string Summary;


        /// <summary> Instance control. </summary>
        public UIElement Instance
        {
            get
            {
                if (this.instance == null) this.instance = new SampleControl(this);
                return this.instance;
            }
        }
        private UIElement instance;
    }
}