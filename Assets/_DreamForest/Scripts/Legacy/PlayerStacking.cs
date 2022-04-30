using System.Collections.Generic;
using _DreamForest.GameServices;
using Legacy;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.Legacy
{
    public class PlayerStacking : MonoBehaviour
    {
        [SerializeField] private Transform stackingParent;
        [SerializeField] private int stackLimit = 20;

        private GlobalEventsService _globalEvents;
        private int _addedGrasses;

        private readonly List<StackableItem> stackedItems = new List<StackableItem>();

        private void Start()
        {
            _globalEvents = Services.Single<GlobalEventsService>();

            _globalEvents.ItemAdded.AddListener(AddToStackIfGrassEnough);
            _globalEvents.StackableItemRemoved.AddListener(PoolizeItemIfHave);
        }

        private void OnDestroy() => 
            _globalEvents.ItemAdded.RemoveListener(AddToStackIfGrassEnough);

        public void AddItem(StackableItem item) => 
            stackedItems.Add(item);

        public Transform GetLastItem() => 
            stackedItems.Count > 0 
                ? stackedItems[stackedItems.Count - 1].transform 
                : stackingParent;

        private void AddToStackIfGrassEnough(SkinType skinType, ResourceObject resourceObject)
        {
            if (skinType != SkinType.None) 
                _addedGrasses++;

            if (_addedGrasses >= 10)
            {
                AddItem(skinType, resourceObject);
                _addedGrasses = 0;
            }
        }

        private void PoolizeItemIfHave()
        {
            if (stackedItems.Count == 0)
                return;

            StackableItem item = stackedItems[0];

            stackedItems.RemoveAt(0);
            item.Poolize();
        }

        private void AddItem(SkinType skinType, ResourceObject resourceObject)
        {
            var item = ItemPool.Instance.UseItem(skinType, resourceObject.transform.position);
            item.PickUp(this);
        }

        private void OnTriggerStay(Collider collider)
        {
            if(collider.gameObject.CompareTag("Consumer"))
            {
                ResourceConsumer resourceConsumer = collider.gameObject.GetComponent<ResourceConsumer>();

                if(!resourceConsumer.CanConsume)
                    return;

                resourceConsumer.ConsumeResource();
            }
        }

        public bool HasEmptySpace() => 
            stackedItems.Count < stackLimit;
    }
}