using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerStacking : MonoBehaviour
{
    private List<StackableItem> stackedItems = new List<StackableItem>();
    [SerializeField] private Transform stackingParent;

    private int stackLimit = 20;

    public Action<bool> Looting;

    public void AddItem(StackableItem item)
    {
        stackedItems.Add(item);
    }

    public Transform GetLastItem()
    {
        if(stackedItems.Count > 0)
        {
            return stackedItems[stackedItems.Count-1].transform;
        }
        else
        {
            return stackingParent;
        }
    }

    public bool HasEmptySpace()
    {
        return stackedItems.Count < stackLimit;
    }

    private void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "Consumer" && stackedItems.Count > 0)
        {
            Consumer consumer = collider.gameObject.GetComponent<Consumer>();
            StackableItem lastItem = stackedItems[stackedItems.Count-1];

            if(!consumer.DoesNeed(lastItem.type)) return;

            stackedItems.Remove(lastItem);
            lastItem.transform.parent = null;

            lastItem.Poolize();

            consumer.ConsumeItem(lastItem);
        }
    }
}