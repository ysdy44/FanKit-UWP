using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FanKit.Transformers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FanKit.Frames.Transformers
{
    /// <summary>
    /// Tools of different shapes.
    /// </summary>
    public enum MarqueeToolType
    {
        /// <summary> Normal. </summary>
        None,

        /// <summary> □ </summary>
        Rectangular,
        /// <summary> ◯ </summary>
        Elliptical,
        /// <summary> 🗨 </summary>
        Polygonal,
        /// <summary> 🗯 </summary>
        FreeHand,
    } 

    /// <summary>
    /// The composite mode used for the marquee.
    /// </summary>
    public enum MarqueeCompositeMode
    {
        /// <summary> New bitmap. </summary>
        New,
        /// <summary> Union of source and destination bitmap. </summary>
        Add,
        /// <summary> Region of the source bitmap. </summary>
        Subtract,
        /// <summary> Intersection of source and destination bitmap.</summary>
        Intersect,

        /// <summary> Union of source and destination bitmaps with xor function for pixels that overlap. </summary>
        Xor,
    }


    public class MarqueeTool
    {



    }
}
