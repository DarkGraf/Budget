using System;

namespace WpfObjectView.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SmartPropertyAttributeAttribute : Attribute
    {
        public string Header { get; set; }
    }
}