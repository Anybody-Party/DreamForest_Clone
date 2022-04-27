using System;
using System.Linq;
using _DreamForest.Data;
using _DreamForest.GameServices;
using _Game.Data;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.Legacy
{
    public class ResourceConsumer : MonoBehaviour
    {
        public event Action<Resource[]> ResourceGained;

        public Resource[] NeededResources;
        [SerializeField] private string _id;

        [Space]
        [SerializeField] private float consumeCooldown = 0.2f;

        private IConsumer consumer;
        private float consumeTimer;
        private WalletService _wallet;
        private GlobalEventsService _globalEvents;
        private DataService _dataService;
        private ResourcesConsumerData _savedData;

        private bool IsFinished => NeededResources.All(x => x.Amount <= 0);
        public bool CanConsume => NeededResources.Any(x => x.Amount > 0 && _wallet.GetAmount(x.Type) > 0);

        private void Start()
        {
            consumer = GetComponent<IConsumer>();

            _wallet = Services.Instance.Single<WalletService>();
            _globalEvents = Services.Instance.Single<GlobalEventsService>();
            _dataService = Services.Instance.Single<DataService>();

            _savedData = _dataService.SavableData.ResourcesConsumers.FirstOrDefault(x => x.Id == _id);
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
                Id = _id,
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

        public void ConsumeItem()
        {
            if (consumeTimer > 0)
                return;

            consumeTimer = consumeCooldown;

            Resource neededResource = NeededResources.First(x => x.Amount > 0);
            float removedValue = Mathf.Min(1, neededResource.Amount);

            _wallet.Remove(removedValue, neededResource.Type);
            neededResource.Amount -= removedValue;

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
            _id = Guid.NewGuid().ToString();
#endif
    }
}