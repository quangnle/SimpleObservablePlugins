using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace PluginOne
{
    public class ComponentOne : IPlugin
    {

        public string Name
        {
            get { return "C1"; }
            set { this.Name = value; }
        }

        [DesignTime(DesignTimeType.Number, false)]
        public int Length { get; set; }

        [DesignTime(DesignTimeType.Number, false)]
        public int Width { get; set; }

        [DesignTime(DesignTimeType.String, false)]
        public string Caption { get; set; }

        public string GennerateCode()
        {
            return "Component ONE";
        }
    }
}
