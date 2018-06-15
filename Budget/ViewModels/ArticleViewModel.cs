using System;
using WpfObjectView.Attributes;

namespace Budget.ViewModels
{
    class ArticleViewModel
    {
        [SmartListItem(Header = "Наименование")]
        public string Name { get; set; }
    }
}