using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace ObservablePlugin
{
    public class PluginProperty
    {
        public string Name { get; set; }
        public DesignTimeType PropType { get; set; }
        public bool IsRequired { get; set; }
    }
}
