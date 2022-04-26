using System;
using RH.Utilities.ServiceLocator;

namespace _Game.Data
{
    public interface IWalletService : IService
    {
        event Action MoneyCountChanged;
        int Money { get; }
        void Add(int amount);
        void Remove(int amount);
        bool EnoughMoney(int price);
    }
}