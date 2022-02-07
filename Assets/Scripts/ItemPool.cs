using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    public static ItemPool Instance { get; private set; }

    [SerializeField]
    private GameObject[] itemPrefabs;

    private List<StackableItem>[] itemPool = new List<StackableItem>[3];
    private static int defaultPoolSize = 5;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        for(var n = 0; n < itemPool.Length; n++)
        {
            itemPool[n] = new List<StackableItem>();
        
            for(var i = 0; i < defaultPoolSize; i++)
            {
                CreateItem((StackableItem.Type)n);
            }
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
        {
            CreateItem(type);
        }

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
