using System;

namespace WpfObjectView.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class VisibleInViewAttribute : Attribute
    {
        public VisibleInViewAttribute(bool visible)
        {
            VisibleInList = visible;
            VisibleInDetail = visible;
        }

        public VisibleInViewAttribute(bool visibleInList, bool visibleInDetail)
        {
            VisibleInList = visibleInList;
            VisibleInDetail = visibleInDetail;
        }

        public bool VisibleInList { get; }
        public bool VisibleInDetail { get; }
    }
}