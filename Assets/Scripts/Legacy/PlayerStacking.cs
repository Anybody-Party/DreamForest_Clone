using System;
using System.Collections.Generic;
using UnityEngine;

namespace Legacy
{
    public class PlayerStacking : MonoBehaviour
    {
        public Action<bool> Looting;

        [SerializeField] private Transform stackingParent;
        [SerializeField] private int stackLimit = 20;

        private readonly List<StackableItem> stackedItems = new List<StackableItem>();

        public void AddItem(StackableItem item) => 
            stackedItems.Add(item);

        public Transform GetLastItem() => 
            stackedItems.Count > 0 
                ? stackedItems[stackedItems.Count - 1].transform 
                : stackingParent;

        public bool HasEmptySpace() => 
            stackedItems.Count < stackLimit;

        private void OnTriggerStay(Collider collider)
        {
            if(collider.gameObject.CompareTag("Consumer") && stackedItems.Count > 0)
            {
                ResourceConsumer resourceConsumer = collider.gameObject.GetComponent<ResourceConsumer>();
                StackableItem lastItem = stackedItems[stackedItems.Count-1];

                if(!resourceConsumer.DoesNeed(lastItem.type)) 
                    return;

                stackedItems.Remove(lastItem);
                lastItem.transform.parent = null;

                lastItem.Poolize();

                resourceConsumer.ConsumeItem(lastItem, this);
            }
        }
    }
}