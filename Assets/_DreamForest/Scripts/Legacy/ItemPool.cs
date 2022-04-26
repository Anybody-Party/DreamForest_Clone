using System;
using System.Collections.Generic;
using RH.Utilities.SingletonAccess;
using UnityEngine;

namespace Legacy
{
    public class ItemPool : MonoBehaviourSingleton<ItemPool>
    {
        [SerializeField] private GameObject[] itemPrefabs;
        [SerializeField] private int defaultPoolSize = 5;

        private List<StackableItem>[] itemPool = new List<StackableItem>[Enum.GetNames(typeof(StackableItem.Type)).Length];

        private void Start()
        {
            for(var n = 0; n < itemPool.Length; n++)
            {
                itemPool[n] = new List<StackableItem>();
        
                for(var i = 0; i < defaultPoolSize; i++) 
                    CreateItem((StackableItem.Type) n);
            }
        }

        private void CreateItem(StackableItem.Type type)
        {
            int id = (int)type;
            GameObject prefab = itemPrefabs[id];

            GameObject item = Instantiate(prefab, transform.position, Quaternion.identity);
            PoolizeItem(item.GetComponent<StackableItem>());
        }

        public StackableItem UseItem(StackableItem.Type type, Vector3 position)
        {
            int id = (int)type;

            if(itemPool[id].Count == 0) 
                CreateItem(type);

            StackableItem item = itemPool[id][0];
            itemPool[id].Remove(item);

            item.transform.position = position;

            item.gameObject.SetActive(true);
            return item;
        }

        public void PoolizeItem(StackableItem item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(transform, true);

            int id = (int)item.type;
            itemPool[id].Add(item);
        }
    }
}
