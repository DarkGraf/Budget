using System;

namespace WpfObjectView.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SmartListItemAttribute : Attribute
    {
        public string Header { get; set; }
    }
}