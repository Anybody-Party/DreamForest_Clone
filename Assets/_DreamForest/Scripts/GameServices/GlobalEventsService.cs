using _DreamForest.Legacy;
using _Game.Data;
using Legacy;
using RH.Utilities.ServiceLocator;
using UnityEngine.Events;

namespace _DreamForest.GameServices
{
    public class GlobalEventsService : IService
    {
        public UnityEvent<ResourceType, WalletService> ResourcesCountChanged = new UnityEvent<ResourceType, WalletService>();
        public UnityEvent<SkinType, ResourceObject> ItemAdded = new UnityEvent<SkinType, ResourceObject>();
        public UnityEvent StackableItemRemoved = new UnityEvent();
    }
}