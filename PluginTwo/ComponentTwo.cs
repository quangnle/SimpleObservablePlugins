using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace PluginTwo
{
    public class ComponentTwo
    {
        public class ComponentOne : IPlugin
        {

            public string Name
            {
                get { return "C2"; }
                set { this.Name = value; }
            }

            [DesignTime(DesignTimeType.Number, false)]
            public int Surface { get; set; }

            [DesignTime(DesignTimeType.String, false)]
            public string LabelName { get; set; }

            public string GennerateCode()
            {
                return "Component TWO";
            }
        }
    }
}
