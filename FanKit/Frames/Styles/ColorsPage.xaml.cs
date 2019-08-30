using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FanKit.Frames.Styles
{
    /// <summary>
    /// Color type.
    /// </summary>
    public class ColorsItem
    {
        public string Text;
        public Color Color;

        public string Summny;
        public SolidColorBrush Foreground;

        public ColorsItem(string text, Color color)
        {
            this.Text = text;
            this.Color = color;

            this.Summny =
                "A:" + color.A + " " +
                "R:" + color.R + " " +
                "G:" + color.G + " " +
                "B:" + color.B;

            this.Foreground = new SolidColorBrush
            (
                color.A > 64 ?
                (
                    (
                        color.R + color.G + color.B < 640 ||
                       System.Math.Abs(color.R - color.G) +
                      System.Math.Abs(color.G - color.B) +
                      System.Math.Abs(color.B - color.R) > 100
                    ) ?
                    Windows.UI.Colors.White :
                    Windows.UI.Colors.Black
                ) :
                Windows.UI.Colors.Gray
            );

        }
    }

    public sealed partial class ColorsPage : Page
    {
        public ColorsPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "Color color = Windows.UI.";
            this.TopRun2.Text = "Colors";
            this.TopRun3.Text = ".AliceBlue;";

            this.ListView.Loaded += (s, e) => this.ListView.ItemsSource = this._itemsSource;
        }

        List<ColorsItem> _itemsSource = new List<ColorsItem>()
            {
                new ColorsItem("AliceBlue",Windows.UI.Colors.AliceBlue),
                new ColorsItem("AntiqueWhite",Windows.UI.Colors.AntiqueWhite),
                new ColorsItem("Aqua",Windows.UI.Colors.Aqua),
                new ColorsItem("Aquamarine",Windows.UI.Colors.Aquamarine),
                new ColorsItem("Azure",Windows.UI.Colors.Azure),
                new ColorsItem("Beige",Windows.UI.Colors.Beige),
                new ColorsItem("Bisque",Windows.UI.Colors.Bisque),
                new ColorsItem("Black",Windows.UI.Colors.Black),
                new ColorsItem("BlanchedAlmond",Windows.UI.Colors.BlanchedAlmond),
                new ColorsItem("Blue",Windows.UI.Colors.Blue),
                new ColorsItem("BlueViolet",Windows.UI.Colors.BlueViolet),
                new ColorsItem("Brown",Windows.UI.Colors.Brown),
                new ColorsItem("BurlyWood",Windows.UI.Colors.BurlyWood),
                new ColorsItem("CadetBlue",Windows.UI.Colors.CadetBlue),
                new ColorsItem("Chartreuse",Windows.UI.Colors.Chartreuse),
                new ColorsItem("Chocolate",Windows.UI.Colors.Chocolate),
                new ColorsItem("Coral",Windows.UI.Colors.Coral),
                new ColorsItem("CornflowerBlue",Windows.UI.Colors.CornflowerBlue),
                new ColorsItem("Cornsilk",Windows.UI.Colors.Cornsilk),
                new ColorsItem("Crimson",Windows.UI.Colors.Crimson),
                new ColorsItem("Cyan",Windows.UI.Colors.Cyan),
                new ColorsItem("DarkBlue",Windows.UI.Colors.DarkBlue),
                new ColorsItem("DarkCyan",Windows.UI.Colors.DarkCyan),
                new ColorsItem("DarkGoldenrod",Windows.UI.Colors.DarkGoldenrod),
                new ColorsItem("DarkGray",Windows.UI.Colors.DarkGray),
                new ColorsItem("DarkGreen",Windows.UI.Colors.DarkGreen),
                new ColorsItem("DarkKhaki",Windows.UI.Colors.DarkKhaki),
                new ColorsItem("DarkMagenta",Windows.UI.Colors.DarkMagenta),
                new ColorsItem("DarkOliveGreen",Windows.UI.Colors.DarkOliveGreen),
                new ColorsItem("DarkOrange",Windows.UI.Colors.DarkOrange),
                new ColorsItem("DarkOrchid",Windows.UI.Colors.DarkOrchid),
                new ColorsItem("DarkRed",Windows.UI.Colors.DarkRed),
                new ColorsItem("DarkSalmon",Windows.UI.Colors.DarkSalmon),
                new ColorsItem("DarkSeaGreen",Windows.UI.Colors.DarkSeaGreen),
                new ColorsItem("DarkSlateBlue",Windows.UI.Colors.DarkSlateBlue),
                new ColorsItem("DarkSlateGray",Windows.UI.Colors.DarkSlateGray),
                new ColorsItem("DarkTurquoise",Windows.UI.Colors.DarkTurquoise),
                new ColorsItem("DarkViolet",Windows.UI.Colors.DarkViolet),
                new ColorsItem("DeepPink",Windows.UI.Colors.DeepPink),
                new ColorsItem("DeepSkyBlue",Windows.UI.Colors.DeepSkyBlue),
                new ColorsItem("DimGray",Windows.UI.Colors.DimGray),
                new ColorsItem("DodgerBlue",Windows.UI.Colors.DodgerBlue),
                new ColorsItem("Firebrick",Windows.UI.Colors.Firebrick),
                new ColorsItem("FloralWhite",Windows.UI.Colors.FloralWhite),
                new ColorsItem("ForestGreen",Windows.UI.Colors.ForestGreen),
                new ColorsItem("Fuchsia",Windows.UI.Colors.Fuchsia),
                new ColorsItem("Gainsboro",Windows.UI.Colors.Gainsboro),
                new ColorsItem("GhostWhite",Windows.UI.Colors.GhostWhite),
                new ColorsItem("Gold",Windows.UI.Colors.Gold),
                new ColorsItem("Goldenrod",Windows.UI.Colors.Goldenrod),
                new ColorsItem("Gray",Windows.UI.Colors.Gray),
                new ColorsItem("Green",Windows.UI.Colors.Green),
                new ColorsItem("GreenYellow",Windows.UI.Colors.GreenYellow),
                new ColorsItem("Honeydew",Windows.UI.Colors.Honeydew),
                new ColorsItem("HotPink",Windows.UI.Colors.HotPink),
                new ColorsItem("IndianRed",Windows.UI.Colors.IndianRed),
                new ColorsItem("Indigo",Windows.UI.Colors.Indigo),
                new ColorsItem("Ivory",Windows.UI.Colors.Ivory),
                new ColorsItem("Khaki",Windows.UI.Colors.Khaki),
                new ColorsItem("Lavender",Windows.UI.Colors.Lavender),
                new ColorsItem("LavenderBlush",Windows.UI.Colors.LavenderBlush),
                new ColorsItem("LawnGreen",Windows.UI.Colors.LawnGreen),
                new ColorsItem("LemonChiffon",Windows.UI.Colors.LemonChiffon),
                new ColorsItem("LightBlue",Windows.UI.Colors.LightBlue),
                new ColorsItem("LightCoral",Windows.UI.Colors.LightCoral),
                new ColorsItem("LightCyan",Windows.UI.Colors.LightCyan),
                new ColorsItem("LightGoldenrodYellow",Windows.UI.Colors.LightGoldenrodYellow),
                new ColorsItem("LightGray",Windows.UI.Colors.LightGray),
                new ColorsItem("LightGreen",Windows.UI.Colors.LightGreen),
                new ColorsItem("LightPink",Windows.UI.Colors.LightPink),
                new ColorsItem("LightSalmon",Windows.UI.Colors.LightSalmon),
                new ColorsItem("LightSeaGreen",Windows.UI.Colors.LightSeaGreen),
                new ColorsItem("LightSkyBlue",Windows.UI.Colors.LightSkyBlue),
                new ColorsItem("LightSlateGray",Windows.UI.Colors.LightSlateGray),
                new ColorsItem("LightSteelBlue",Windows.UI.Colors.LightSteelBlue),
                new ColorsItem("LightYellow",Windows.UI.Colors.LightYellow),
                new ColorsItem("Lime",Windows.UI.Colors.Lime),
                new ColorsItem("LimeGreen",Windows.UI.Colors.LimeGreen),
                new ColorsItem("Linen",Windows.UI.Colors.Linen),
                new ColorsItem("Magenta",Windows.UI.Colors.Magenta),
                new ColorsItem("Maroon",Windows.UI.Colors.Maroon),
                new ColorsItem("MediumAquamarine",Windows.UI.Colors.MediumAquamarine),
                new ColorsItem("MediumBlue",Windows.UI.Colors.MediumBlue),
                new ColorsItem("MediumOrchid",Windows.UI.Colors.MediumOrchid),
                new ColorsItem("MediumPurple",Windows.UI.Colors.MediumPurple),
                new ColorsItem("MediumSeaGreen",Windows.UI.Colors.MediumSeaGreen),
                new ColorsItem("MediumSlateBlue",Windows.UI.Colors.MediumSlateBlue),
                new ColorsItem("MediumSpringGreen",Windows.UI.Colors.MediumSpringGreen),
                new ColorsItem("MediumTurquoise",Windows.UI.Colors.MediumTurquoise),
                new ColorsItem("MediumVioletRed",Windows.UI.Colors.MediumVioletRed),
                new ColorsItem("MidnightBlue",Windows.UI.Colors.MidnightBlue),
                new ColorsItem("MintCream",Windows.UI.Colors.MintCream),
                new ColorsItem("MistyRose",Windows.UI.Colors.MistyRose),
                new ColorsItem("Moccasin",Windows.UI.Colors.Moccasin),
                new ColorsItem("NavajoWhite",Windows.UI.Colors.NavajoWhite),
                new ColorsItem("Navy",Windows.UI.Colors.Navy),
                new ColorsItem("OldLace",Windows.UI.Colors.OldLace),
                new ColorsItem("Olive",Windows.UI.Colors.Olive),
                new ColorsItem("OliveDrab",Windows.UI.Colors.OliveDrab),
                new ColorsItem("Orange",Windows.UI.Colors.Orange),
                new ColorsItem("OrangeRed",Windows.UI.Colors.OrangeRed),
                new ColorsItem("Orchid",Windows.UI.Colors.Orchid),
                new ColorsItem("PaleGoldenrod",Windows.UI.Colors.PaleGoldenrod),
                new ColorsItem("PaleGreen",Windows.UI.Colors.PaleGreen),
                new ColorsItem("PaleTurquoise",Windows.UI.Colors.PaleTurquoise),
                new ColorsItem("PaleVioletRed",Windows.UI.Colors.PaleVioletRed),
                new ColorsItem("PapayaWhip",Windows.UI.Colors.PapayaWhip),
                new ColorsItem("PeachPuff",Windows.UI.Colors.PeachPuff),
                new ColorsItem("Peru",Windows.UI.Colors.Peru),
                new ColorsItem("Pink",Windows.UI.Colors.Pink),
                new ColorsItem("Plum",Windows.UI.Colors.Plum),
                new ColorsItem("PowderBlue",Windows.UI.Colors.PowderBlue),
                new ColorsItem("Purple",Windows.UI.Colors.Purple),
                new ColorsItem("Red",Windows.UI.Colors.Red),
                new ColorsItem("RosyBrown",Windows.UI.Colors.RosyBrown),
                new ColorsItem("RoyalBlue",Windows.UI.Colors.RoyalBlue),
                new ColorsItem("SaddleBrown",Windows.UI.Colors.SaddleBrown),
                new ColorsItem("Salmon",Windows.UI.Colors.Salmon),
                new ColorsItem("SandyBrown",Windows.UI.Colors.SandyBrown),
                new ColorsItem("SeaGreen",Windows.UI.Colors.SeaGreen),
                new ColorsItem("SeaShell",Windows.UI.Colors.SeaShell),
                new ColorsItem("Sienna",Windows.UI.Colors.Sienna),
                new ColorsItem("Silver",Windows.UI.Colors.Silver),
                new ColorsItem("SkyBlue",Windows.UI.Colors.SkyBlue),
                new ColorsItem("SlateBlue",Windows.UI.Colors.SlateBlue),
                new ColorsItem("SlateGray",Windows.UI.Colors.SlateGray),
                new ColorsItem("Snow",Windows.UI.Colors.Snow),
                new ColorsItem("SpringGreen",Windows.UI.Colors.SpringGreen),
                new ColorsItem("SteelBlue",Windows.UI.Colors.SteelBlue),
                new ColorsItem("Tan",Windows.UI.Colors.Tan),
                new ColorsItem("Teal",Windows.UI.Colors.Teal),
                new ColorsItem("Thistle",Windows.UI.Colors.Thistle),
                new ColorsItem("Tomato",Windows.UI.Colors.Tomato),
                new ColorsItem("Transparent",Windows.UI.Colors.Transparent),
                new ColorsItem("Turquoise",Windows.UI.Colors.Turquoise),
                new ColorsItem("Violet",Windows.UI.Colors.Violet),
                new ColorsItem("Wheat",Windows.UI.Colors.Wheat),
                new ColorsItem("White",Windows.UI.Colors.White),
                new ColorsItem("WhiteSmoke",Windows.UI.Colors.WhiteSmoke),
                new ColorsItem("Yellow",Windows.UI.Colors.Yellow),
                new ColorsItem("YellowGreen",Windows.UI.Colors.YellowGreen),
            };
    }
}