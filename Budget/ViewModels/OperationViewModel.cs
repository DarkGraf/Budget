using System;
using System.ComponentModel.DataAnnotations;
using Budget.Bll.DomainObjects;
using WpfObjectView.Attributes;
using WpfObjectView.ViewModels;

namespace Budget.ViewModels
{
    class OperationViewModel : ObjectViewModelBase<FinanceOperation>
    {
        public OperationViewModel() { }

        public OperationViewModel(FinanceOperation operation) : base(operation) { }

        public override long RealKey => Object.Id;

        protected override string DisplayName => $"{Date.ToString()} {Article.Name} {Sum.ToString()}";
        
        [Display(Name = "Дата")]
        public DateTime Date
        {
            get { return Object.Date; }
            set
            {
                if (Object.Date != value)
                {
                    Object.Date = value;
                    RaisePropertyChanged();
                }
            }
        }

        [Display(Name = "Статья")]
        [DataSource(typeof(ArticlesViewModel))]
        public ArticleViewModel Article
        {
            get { return new ArticleViewModel(Object.Article); }
            set
            {
                if (Object.Article != value.Object)
                {
                    Object.Article = value.Object;
                    RaisePropertyChanged();
                }
            }
        }

        [Display(Name = "Сумма")]
        public decimal Sum
        {
            get { return Object.Sum; }
            set
            {
                if (Object.Sum != value)
                {
                    Object.Sum = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}