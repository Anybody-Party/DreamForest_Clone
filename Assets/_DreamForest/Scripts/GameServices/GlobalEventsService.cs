using _Game.Data;
using Legacy;
using RH.Utilities.ServiceLocator;
using UnityEngine.Events;

namespace _DreamForest.GameServices
{
    public class GlobalEventsService : IService
    {
        public UnityEvent<PlayerStacking> StackingItemsChanged = new UnityEvent<PlayerStacking>();
        public UnityEvent<IWalletService> MoneyCountChanged = new UnityEvent<IWalletService>();
    }
}