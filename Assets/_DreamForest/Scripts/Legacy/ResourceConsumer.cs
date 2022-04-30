using System;
using System.Linq;
using _DreamForest.Data;
using _DreamForest.GameServices;
using _Game.Data;
using RH.Utilities.Attributes;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.Legacy
{
    public class ResourceConsumer : MonoBehaviour
    {
        public event Action<Resource[]> ResourceGained;

        public Resource[] NeededResources;
        [ReadOnly] public string Id;

        [Space]
        [SerializeField] private float consumeCooldown = 0.2f;

        private IConsumer consumer;
        private float consumeTimer;
        private WalletService _wallet;
        private GlobalEventsService _globalEvents;
        private DataService _dataService;
        private ResourcesConsumerData _savedData;

        private bool IsFinished => NeededResources.All(x => x.Amount <= 0);
        public bool CanConsume => NeededResources.Any(x => x.Amount > 0 && _wallet.GetAmount(x.Type) >= 1);

        private void Start()
        {
            consumer = GetComponent<IConsumer>();

            _wallet = Services.Single<WalletService>();
            _globalEvents = Services.Single<GlobalEventsService>();
            _dataService = Services.Single<DataService>();

            _savedData = _dataService.SavableData.ResourcesConsumers
                .FirstOrDefault(x => x.Id == Id);

            if (_savedData != null)
                Load(@by: _savedData);
            else
                CreateSaveData();

            ResourceGained?.Invoke(NeededResources);
        }

        private void CreateSaveData()
        {
            _savedData = new ResourcesConsumerData
            {
                Id = Id,
                NeededResources = NeededResources,
            };

            _dataService.SavableData.ResourcesConsumers.Add(_savedData);
        }

        private void Load(ResourcesConsumerData @by)
        {
            NeededResources = @by.NeededResources;

            if (IsFinished)
                Finish();
        }

        private void Update()
        {
            if(consumeTimer > 0)
                consumeTimer -= Time.deltaTime;
        }

        public void ConsumeResource()
        {
            if (consumeTimer > 0)
                return;

            consumeTimer = consumeCooldown;
            Resource neededResource = NeededResources.First(x => x.Amount > 0 && _wallet.GetAmount(x.Type) >= 1);

            _wallet.Remove(1f, neededResource.Type);
            neededResource.Amount -= 1f;

            InvokeOnConsumedEvents(neededResource);
            Save();

            if(IsFinished)
                Finish();
        }

        private void InvokeOnConsumedEvents(Resource neededResource)
        {
            if (neededResource.Type == ResourceType.Grass)
                _globalEvents.StackableItemRemoved?.Invoke();

            consumer.PerformOnReceiveResource();
            ResourceGained?.Invoke(NeededResources);
        }

        private void Save() => 
            _savedData.NeededResources = NeededResources;

        private void Finish() => 
            consumer.PerformOnFilled();

#if UNITY_EDITOR
        [ContextMenu("Create id")]
        private void CreateId() => 
            Id = Guid.NewGuid().ToString();
#endif
    }
}