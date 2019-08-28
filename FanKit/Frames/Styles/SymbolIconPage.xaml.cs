using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace FanKit.Frames.Styles
{
    public sealed partial class SymbolIconPage : Page
    {
        public SymbolIconPage()
        {
            this.InitializeComponent();

            this.TopRun1.Text = "<SymbolIcon  Symbol=\"Symbol.";
            this.TopRun2.Text = "Previous";
            this.TopRun3.Text = "\" /> ";

            this.GridView.Loaded += (s, e) =>
            {
                List<Symbol> symbols = new List<Symbol>();
                foreach (Symbol item in Enum.GetValues(typeof(Symbol)))
                {
                    symbols.Add(item);
                }
                this.GridView.ItemsSource = symbols;
            };
        }        
    }
}