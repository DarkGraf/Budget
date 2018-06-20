using Budget.Bll.DomainObjects;
using System.ComponentModel.DataAnnotations;
using WpfObjectView.ViewModels;

namespace Budget.ViewModels
{
    class StorageViewModel : ObjectViewModelBase<FinanceStorage>
    {
        public StorageViewModel() { }

        public StorageViewModel(FinanceStorage storage) : base(storage) { }

        protected override string DisplayName
        {
            get { return Name; }
        }

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
    }
}