using Budget.Bll.DomainObjects;
using Prism.Mvvm;
using System;

namespace Budget.ViewModels
{
    class StorageViewModel : BindableBase
    {
        public FinanceStorage Storage { get; }

        public StorageViewModel(FinanceStorage storage)
        {
            Storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

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