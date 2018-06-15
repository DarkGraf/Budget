using System;
using System.Collections.Generic;
using WpfObjectView.ViewModels;

namespace Budget.ViewModels
{
    class ArticlesViewModel : ObjectListViewModel<ArticleViewModel>
    {
        public ArticlesViewModel()
        {

        }

        protected override IEnumerable<ArticleViewModel> GetItems()
        {
            return new List<ArticleViewModel>
            {
                new ArticleViewModel { Name = "Aaa" },
                new ArticleViewModel { Name = "Bbb" },
                new ArticleViewModel { Name = "Ccc" },
                new ArticleViewModel { Name = "Ddd" },
                new ArticleViewModel { Name = "Eee" }
            };
        }
    }
}