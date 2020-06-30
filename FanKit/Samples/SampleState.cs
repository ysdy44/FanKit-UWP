namespace FanKit.Samples
{
    /// <summary>
    /// State of <see cref=" FanKit.Samples.Sample"/>.
    /// </summary>
    public enum SampleState
    {
        /// <summary> Default state </summary>
        None,

        /// <summary> New sample </summary>
        New,
        /// <summary> The sample updated</summary>
        Updated,

        /// <summary> Preview sample </summary>
        Preview,
        /// <summary> Recom sample </summary>
        Recom,
        /// <summary> Disable sample </summary>
        Disable,
    }
}