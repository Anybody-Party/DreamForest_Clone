using System;
using _DreamForest.GameServices;
using RH.Utilities.ServiceLocator;

namespace _Game.Data
{
    public class WalletService : IWalletService
    {
        private readonly DataService _data;
        private readonly GlobalEventsService _globalEvents;

        public WalletService()
        {
            _data = Services.Instance.Single<DataService>();
            _globalEvents = Services.Instance.Single<GlobalEventsService>();
        }

        public int Money
        {
            get => _data.SavableData.Money;
            private set => _data.SavableData.Money = value;
        }

        public void Add(int amount) => 
            ChangeMoneyAmount(amount);

        public void Remove(int amount)
        {
            if (!EnoughMoney(amount))
                throw new Exception($"Can't remove {amount} coins. Not enough money!");

            ChangeMoneyAmount(-amount);
        }

        public bool EnoughMoney(int price) => 
            price >= Money;

        private void ChangeMoneyAmount(int amount)
        {
            Money += amount;
            _globalEvents.MoneyCountChanged?.Invoke(this);
        }
    }
}