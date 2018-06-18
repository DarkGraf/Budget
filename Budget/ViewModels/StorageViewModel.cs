using Budget.Bll.DomainObjects;
using Prism.Mvvm;
using System;
using System.ComponentModel.DataAnnotations;
using WpfObjectView.Attributes;

namespace Budget.ViewModels
{
    class StorageViewModel : BindableBase
    {
        [VisibleInView(false)]
        public FinanceStorage Storage { get; }

        public StorageViewModel()
        {
            Storage = new FinanceStorage();
        }

        public StorageViewModel(FinanceStorage storage)
        {
            Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        [VisibleInView(false)]
        public long Id
        {
            get { return Storage.Id; }
            set
            {
                if (Storage.Id != value)
                {
                    Storage.Id = value;
                    RaisePropertyChanged(nameof(Storage.Id));
                }
            }
        }

        [Display(Name = "Наименование")]
        public string Name
        {
            get { return Storage.Name; }
            set
            {
                if (Storage.Name != value)
                {
                    Storage.Name = value;
                    RaisePropertyChanged(nameof(Storage.Name));
                }
            }
        }
    }
}