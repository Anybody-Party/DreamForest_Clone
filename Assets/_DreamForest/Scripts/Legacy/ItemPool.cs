using System.Collections.Generic;
using System.Linq;
using RH.Utilities.SingletonAccess;
using UnityEngine;

namespace _DreamForest.Legacy
{
    public class ItemPool : MonoBehaviourSingleton<ItemPool>
    {
        [SerializeField] private StackableItem[] _skinsPrefabs;
        [SerializeField] private int _defaultPoolSize = 10;

        private readonly Dictionary<SkinType, List<StackableItem>> _itemPool = new Dictionary<SkinType, List<StackableItem>>
        {
            [SkinType.GrassBlue] = new List<StackableItem>(),
            [SkinType.GrassGreen] = new List<StackableItem>(),
        };

        private void Start()
        {
            foreach (StackableItem skin in _skinsPrefabs)
                for (var i = 0; i < _defaultPoolSize; i++)
                    CreateItem(skin.type);
        }

        private void CreateItem(SkinType type)
        {
            int id = (int)type;
            StackableItem prefab = _skinsPrefabs.First(x => (int)x.type == id);

            StackableItem item = Instantiate(prefab, transform.position, Quaternion.identity);
            PoolizeItem(item);
        }

        public StackableItem UseItem(SkinType type, Vector3 position)
        {
            if(_itemPool[type].Count == 0) 
                CreateItem(type);

            StackableItem item = _itemPool[type][0];
            _itemPool[type].Remove(item);

            item.transform.position = position;

            item.gameObject.SetActive(true);
            return item;
        }

        public void PoolizeItem(StackableItem item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(transform, true);

            _itemPool[item.type].Add(item);
        }
    }
}
