using Budget.Bll.DomainObjects;
using Budget.Bll.DomainObjects.Enums;
using Prism.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;
using WpfObjectView.Attributes;

namespace Budget.ViewModels
{
    class ArticleViewModel : BindableBase
    {
        [VisibleInView(false)]
        public FinanceArticle Article { get; }

        public ArticleViewModel()
        {
            Article = new FinanceArticle();
        }

        public ArticleViewModel(FinanceArticle article)
        {
            Article = article ?? throw new ArgumentNullException(nameof(article));
        }

        [VisibleInView(false)]
        public long Id
        {
            get { return Article.Id; }
        }

        [Display(Name = "Наименование")]
        public string Name
        {
            get { return Article.Name; }
            set
            {
                if (Article.Name != value)
                {
                    Article.Name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        [Display(Name = "Тип")]
        public FinanceArticleType Type
        {
            get { return Article.Type; }
            set
            {
                if (Article.Type != value)
                {
                    Article.Type = value;
                    RaisePropertyChanged(nameof(Type));
                }
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}