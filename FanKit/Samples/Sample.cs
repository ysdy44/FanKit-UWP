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

        /// <summary> Flyout summary event. </summary>
        public static event TypedEventHandler<object, Sample> FlyoutSample;
        /// <summary> Flyout summary method. </summary>
        public static void FlyoutSample_Invoke(object sender, Sample sample) => Sample.FlyoutSample?.Invoke(sender, sample);



        /// <summary> Sample's state. </summary>
        [JsonProperty]
        public SampleState State { get; set; }

        /// <summary> Sample's frame page. </summary>
        [JsonProperty]
        public Type Page { get; set; }

        /// <summary> Sample's name. </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary> Sample's image uri. </summary>
        [JsonProperty]
        public Uri Uri { get; set; }

        /// <summary> Sample's summary. </summary>
        [JsonProperty]
        public string Summary { get; set; }


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