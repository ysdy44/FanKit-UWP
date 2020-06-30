using Newtonsoft.Json;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace FanKit.Samples
{
    /// <summary>
    /// Category of <see cref="Sample"/>.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class SamplesCategory
    {
        //@Converter
        public Visibility BoolToVisibilityConverter(bool value) => value ? Visibility.Visible : Visibility.Collapsed;
        
        /// <summary> SampleCategory's name. </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary> SampleCategory's bedge. </summary>
        [JsonProperty]
        public bool HasBedge { get; set; }

        /// <summary> SampleCategory's list. </summary>
        [JsonProperty]
        public List<Sample> Samples { get; set; }
    }
}