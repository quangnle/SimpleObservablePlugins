using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PluginInterface
{
    public class DesignTimeAttribute : Attribute
    {
        public DesignTimeType AttributeType { get; private set; }
        public bool IsRequired { get; set; }

        public DesignTimeAttribute(DesignTimeType type, bool isRequired)
        {
            this.AttributeType = type;
            this.IsRequired = isRequired;
        }
    }
}
