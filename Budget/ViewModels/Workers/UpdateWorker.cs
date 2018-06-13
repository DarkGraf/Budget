﻿using Budget.Bll.DomainObjects;
using System;

namespace Budget.ViewModels.Workers
{
    class UpdateWorker : IWorker
    {
        readonly FinanceStorage storage;
        private long id;

        public UpdateWorker(FinanceStorage storage)
        {
            this.storage = storage ?? throw new ArgumentNullException(nameof(storage));
        }

        public void Init(IStorageViewModel viewModel)
        {
            id = storage.Id;
            viewModel.Name = storage.Name;
        }

        public void Save(IStorageViewModel viewModel)
        {
            FinanceStorage storage = new FinanceStorage
            {
                Id = id,
                Name = viewModel.Name
            };
            viewModel.BudgetObject.UpdateFinanceStorage(storage);
            viewModel.Name = string.Empty;
        }
    }
}