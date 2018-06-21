using Budget.Bll.DomainObjects;
using Budget.Bll.DomainObjects.Enums;
using System.ComponentModel.DataAnnotations;
using WpfObjectView.ViewModels;

namespace Budget.ViewModels
{
    class ArticleViewModel : ObjectViewModelBase<FinanceArticle>
    {
        public ArticleViewModel() { }

        public ArticleViewModel(FinanceArticle article) : base(article) { }

        public override long RealKey => Object.Id;

        protected override string DisplayName => Name;

        [Display(Name = "Наименование")]
        public string Name
        {
            get { return Object.Name; }
            set
            {
                if (Object.Name != value)
                {
                    Object.Name = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Display(Name = "Тип")]
        public FinanceArticleType Type
        {
            get { return Object.Type; }
            set
            {
                if (Object.Type != value)
                {
                    Object.Type = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}