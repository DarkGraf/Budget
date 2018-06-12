using Budget.Bll.DomainObjects;
using Budget.Notifications;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Windows.Input;

namespace Budget.ViewModels
{
    class AddOrUpdateStorageViewModel : BindableBase, IInteractionRequestAware
    {
        #region Внутренние классы.

        interface ISaver
        {
            void Init(AddOrUpdateStorageViewModel viewModel);
            void Save(AddOrUpdateStorageViewModel viewModel);
        }

        class AddSaver : ISaver
        {
            public void Init(AddOrUpdateStorageViewModel viewModel) { }

            public void Save(AddOrUpdateStorageViewModel viewModel)
            {
                FinanceStorage storage = new FinanceStorage
                {
                    Name = viewModel.Name
                };
                viewModel.budgetObject.AddFinanceStorage(storage);
            }
        }

        class UpdateSaver : ISaver
        {
            public void Init(AddOrUpdateStorageViewModel viewModel)
            {
                IUpdateObjectNotification<FinanceStorage> updateObject = viewModel.notification as IUpdateObjectNotification<FinanceStorage>;
                viewModel.id = updateObject.Object.Id;
                viewModel.Name = updateObject.Object.Name;
            }

            public void Save(AddOrUpdateStorageViewModel viewModel)
            {
                FinanceStorage storage = new FinanceStorage
                {
                    Id = viewModel.id,
                    Name = viewModel.Name
                };
                viewModel.budgetObject.UpdateFinanceStorage(storage);
            }
        }

        class SaverFactory
        {
            public static ISaver Create(INotification notification)
            {
                if (notification is IAddObjectNotification)
                {
                    return new AddSaver();
                }
                if (notification is IUpdateObjectNotification<FinanceStorage>)
                {
                    return new UpdateSaver();
                }
                throw new InvalidOperationException();
            }
        }

        #endregion

        readonly BudgetObject budgetObject;
        private INotification notification;
        private ISaver saver;

        private string name;

        public AddOrUpdateStorageViewModel(BudgetObject budgetObject)
        {
            this.budgetObject = budgetObject ?? throw new ArgumentNullException(nameof(budgetObject));

            OkCommand = new DelegateCommand(OkExecute);
            CancelCommand = new DelegateCommand(CancelExecute);
        }

        #region IInteractionRequestAware

        public INotification Notification
        {
            get { return notification; }
            set
            {
                notification = value;
                saver = SaverFactory.Create(notification);
                saver.Init(this);
            }
        }

        public Action FinishInteraction { get; set; }

        #endregion

        /// <summary>
        /// Идентификатор хранения, для обновления.
        /// </summary>
        private long id;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        private void OkExecute()
        {
            saver.Save(this);
            FinishInteraction();
            ClearFields();
        }

        private void CancelExecute()
        {
            FinishInteraction();
            ClearFields();
        }

        private void ClearFields()
        {
            Name = string.Empty;
            saver = null;
        }
    }
}
