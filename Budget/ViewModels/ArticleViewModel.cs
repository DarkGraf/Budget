using Budget.Bll.DomainObjects;
using Prism.Mvvm;
using System;
using WpfObjectView.Attributes;

namespace Budget.ViewModels
{
    class ArticleViewModel : BindableBase
    {
        public FinanceArticle Article { get; }

        public ArticleViewModel()
        {
            Article = new FinanceArticle();
        }

        public ArticleViewModel(FinanceArticle article)
        {
            Article = article ?? throw new ArgumentNullException(nameof(article));
        }

        public long Id
        {
            get { return Article.Id; }
        }

        [SmartPropertyAttribute(Header = "Наименование")]
        public string Name
        {
            get { return Article.Name; }
            set
            {
                if (Article.Name != value)
                {
                    Article.Name = value;
                    RaisePropertyChanged(nameof(Article.Name));
                }
            }
        }
    }
}