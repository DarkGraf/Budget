using Budget.Bll.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfObjectView.ViewModels;

namespace Budget.ViewModels
{
    class ArticlesViewModel : ObjectListViewModel<ArticleViewModel>
    {
        readonly BudgetObject budgetObject;

        public ArticlesViewModel(BudgetObject budgetObject)
        {
            this.budgetObject = budgetObject ?? throw new ArgumentNullException(nameof(budgetObject));
            budgetObject.FinanceArticleChanged += (s, e) => RaisePropertyChanged(nameof(Items));
        }

        protected override IEnumerable<ArticleViewModel> GetItems()
        {
            return budgetObject.GetFinanceArticle().Select(a => new ArticleViewModel(a));
        }

        protected override void AddItem(ArticleViewModel item)
        {
            budgetObject.AddFinanceArticle(item.Object);
        }

        protected override void EditItem(ArticleViewModel item)
        {
            budgetObject.UpdateFinanceArticle(item.Object);
        }

        protected override void DeleteItem(ArticleViewModel item)
        {
            budgetObject.DeleteFinanceArticle(item.Object);
        }
    }
}