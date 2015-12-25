using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PluginInterface;

namespace ObservablePlugin
{
    public class PropertyPane
    {
        private double _minWidth = 100;
        private double _minHeight = 25;
        public double MaxWidth 
        {
            get { if (_minWidth == 0) _minWidth = 100; return _minWidth; }
            set { _minWidth = value; } 
        }
        public double MaxHeight 
        {
            get { if (_minHeight == 0) _minHeight = 25; return _minHeight; }
            set { _minHeight = value; } 
        }

        public Grid CreatePane(PluginObserver plugin, IPlugin instance)
        {
            Grid grid = null;
            if (plugin !=null)
            {
                grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition() { MaxWidth = MaxWidth });
                grid.ColumnDefinitions.Add(new ColumnDefinition() { MaxWidth = MaxWidth });

                foreach (var prop in plugin.Properties)
                {   
                    grid.RowDefinitions.Add(new RowDefinition() { MaxHeight = MaxHeight });

                    var label = new Label { Content = prop.Name, HorizontalAlignment = System.Windows.HorizontalAlignment.Right };
                    grid.Children.Add(label);
                    Grid.SetColumn(label, 0);
                    Grid.SetRow(label, grid.RowDefinitions.Count - 1);

                    var textbox = new TextBox() { Name = prop.Name, Tag = new { T = prop.PropType, P = plugin, I = instance } };
                    grid.Children.Add(textbox);
                    Grid.SetColumn(textbox, 1);
                    Grid.SetRow(textbox, grid.RowDefinitions.Count - 1);
                    var propValue = plugin.GetPropertyValue(prop.Name);
                    textbox.Text = propValue == null ? "" : propValue.ToString();
                    textbox.TextChanged += textbox_TextChanged;
                    
                }
            }

            return grid;
        }

        void textbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as TextBox;
            var tag = (dynamic)textbox.Tag;
            if (tag.T == DesignTimeType.Number)
            {
                int output = 0;
                if (Int32.TryParse(textbox.Text, out output))
                    (tag.P as PluginObserver).SetPropertyValue(textbox.Name, output);
                else
                    throw new Exception("Invalid input.");
            }
            else
            {
                (tag.P as PluginObserver).SetPropertyValue(textbox.Name, textbox.Text);
            }
        }
    }
}
