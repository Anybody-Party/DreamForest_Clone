using System.Collections.Generic;
using _DreamForest.GameServices;
using _DreamForest.Legacy;
using Legacy;
using RH.Utilities.ServiceLocator;
using Resource = _DreamForest.Data.Resource;

namespace _Game.Data
{
    public class WalletService : IService
    {
        private readonly DataService _data;
        private readonly GlobalEventsService _globalEvents;

        public WalletService()
        {
            _data = Services.Single<DataService>();
            _globalEvents = Services.Single<GlobalEventsService>();
        }

        public List<Resource> Resources => _data.SavableData.Resources;

        public float GetAmount(ResourceType type) => Resources.Find(x => x.Type == type).Amount;

        public void Add(float amount, ResourceType type) => 
            ChangeResourceAmount(amount, type);

        public void Remove(float amount, ResourceType type) => 
            ChangeResourceAmount(-amount, type);

        private void ChangeResourceAmount(float amount, ResourceType type)
        {
            Resources.Find(x => x.Type == type).Amount += amount;
            _globalEvents.ResourcesCountChanged?.Invoke(type, this);
        }
    }
}