using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PluginInterface;

namespace ObservablePlugin
{
    public class PluginObserver
    {
        private Type _type = null;
        public List<PluginProperty> Properties { get; private set; }
        public IPlugin Instance { get; private set; }

        public PluginObserver(Type type)
        {
            _type = type;
            Initialize();
        }

        public void Initialize()
        {   
            // get all design time properties
            Properties = new List<PluginProperty>();
            foreach (var prop in _type.GetProperties())
            {
                var attr = prop.CustomAttributes.FirstOrDefault(a => a.AttributeType == typeof(DesignTimeAttribute));
                if (attr != null)
                {
                    Properties.Add(new PluginProperty { 
                                            Name = prop.Name, 
                                            PropType = (DesignTimeType)attr.ConstructorArguments[0].Value, 
                                            IsRequired = (bool)attr.ConstructorArguments[1].Value 
                    });
                }
            }

            // create instance
            Instance =  _type.Assembly.CreateInstance(_type.ToString()) as IPlugin;
        }

        public object GetPropertyValue(string propertyName)
        {
            var prop = _type.GetProperty(propertyName);
            if (prop != null)
                return prop.GetValue(Instance);
            else
                throw new Exception("Property is not existed.");
        }

        public void SetPropertyValue(string propertyName, object value)
        {
            var prop = _type.GetProperty(propertyName);
            if (prop != null)
                prop.SetValue(Instance, value);
            else
                throw new Exception("Property is not existed.");
        }

        public object this[string propertyName]
        {
            get { return GetPropertyValue(propertyName); }
            set { SetPropertyValue(propertyName, value); }
        }
    }
}
