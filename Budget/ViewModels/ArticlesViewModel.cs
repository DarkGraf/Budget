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

        protected override void AddSaveItem(ArticleViewModel item)
        {
            budgetObject.AddFinanceArticle(item.Article);
        }

        protected override void EditSaveItem(ArticleViewModel item)
        {
            budgetObject.UpdateFinanceArticle(item.Article);
        }

        protected override void DeleteSaveItem(ArticleViewModel item)
        {
            budgetObject.DeleteFinanceArticle(item.Article);
        }
    }
}