using Newtonsoft.Json;
using System;
using Windows.UI.Xaml.Controls;

namespace FanKit.Samples
{
    /// <summary>
    /// Simple sample item.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public partial class Sample : UserControl
    {
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
    }
}