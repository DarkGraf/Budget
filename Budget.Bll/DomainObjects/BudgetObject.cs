using System;

namespace Budget.Bll.DomainObjects
{
    public sealed class BudgetObject
    {
        readonly IBudgetDataProvider dataProvider;

        public BudgetObject(IBudgetDataProvider dataProvider)
        {
            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        public FinanceStorage[] GetFinanceStorage()
        {
            return dataProvider.GetFinanceStorages();
        }

        public void AddFinanceStorage(FinanceStorage storage)
        {
            dataProvider.AddFinanceStorage(storage.Name);
            FinanceStoragesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void UpdateFinanceStorage(FinanceStorage storage)
        {
            dataProvider.UpdateFinanceStorage(storage.Id, storage.Name);
            FinanceStoragesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void DeleteFinanceStorage(FinanceStorage storage)
        {
            dataProvider.DeleteFinanceStorage(storage.Id);
            FinanceStoragesChanged?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler FinanceStoragesChanged;
    }
}