using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObservablePlugin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PropertyPane _generator = new PropertyPane();
        public MainWindow()
        {
            InitializeComponent();
            LoadPlugins();
        }

        private void LoadPlugins()
        {
            var folder = ConfigurationManager.AppSettings["pluginFolder"];
            var files = from f in Directory.GetFiles(folder) where f.ToLower().EndsWith(".dll") select f;
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFile(file);

                var acceptedType = (from type in assembly.GetExportedTypes()
                                    where type.GetInterface("IPlugin") != null && !type.IsAbstract && !type.IsInterface
                                    select type).FirstOrDefault();

                if (acceptedType != null)
                {
                    var observer = new PluginObserver(acceptedType);
                    cbo.Items.Add(new { Name = observer.Instance.Name, Plugin = observer });
                    cbo.SelectionChanged += cbo_SelectionChanged;
                }
            }
        }

        void cbo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedValue = ((dynamic)cbo.SelectedItem).Plugin;
            var grid = _generator.CreatePane(selectedValue, ((PluginObserver)selectedValue).Instance);
            propertyPane.Children.Clear();
            propertyPane.Children.Add(grid);
        }
    }
}
