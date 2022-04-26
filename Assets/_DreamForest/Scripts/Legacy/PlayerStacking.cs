using System;
using System.Collections.Generic;
using _DreamForest.GameServices;
using _Game.Data;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace Legacy
{
    public class PlayerStacking : MonoBehaviour
    {
        [SerializeField] private Transform stackingParent;
        [SerializeField] private int stackLimit = 20;

        private readonly List<StackableItem> stackedItems = new List<StackableItem>();

        private GlobalEventsService _globalEvents;
        private DataService _dataService;

        private void Start()
        {
            _globalEvents = Services.Instance.Single<GlobalEventsService>();
            _dataService = Services.Instance.Single<DataService>();
        }

        public void AddItem(StackableItem item)
        {
            stackedItems.Add(item);
            _dataService.GrassCount++;
            _globalEvents.StackingItemsChanged?.Invoke(this);
        }

        public Transform GetLastItem() => 
            stackedItems.Count > 0 
                ? stackedItems[stackedItems.Count - 1].transform 
                : stackingParent;

        private void OnTriggerStay(Collider collider)
        {
            if(collider.gameObject.CompareTag("Consumer") && stackedItems.Count > 0)
            {
                ResourceConsumer resourceConsumer = collider.gameObject.GetComponent<ResourceConsumer>();
                StackableItem lastItem = stackedItems[stackedItems.Count-1];

                if(!resourceConsumer.NeedResources)
                    return;

                stackedItems.Remove(lastItem);
                lastItem.transform.parent = null;
                _dataService.GrassCount--;
                _globalEvents.StackingItemsChanged?.Invoke(this);

                lastItem.Poolize();

                resourceConsumer.ConsumeItem(lastItem, this);
            }
        }

        public bool HasEmptySpace() => 
            stackedItems.Count < stackLimit;
    }
}