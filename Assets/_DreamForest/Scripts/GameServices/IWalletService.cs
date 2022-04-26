using System;
using RH.Utilities.ServiceLocator;

namespace _Game.Data
{
    public interface IWalletService : IService
    {
        int Money { get; }
        void Add(int amount);
        void Remove(int amount);
        bool EnoughMoney(int price);
    }
}