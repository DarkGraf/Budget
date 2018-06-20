using System;

namespace WpfObjectView.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataSourceAttribute : Attribute
    {
        public DataSourceAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}