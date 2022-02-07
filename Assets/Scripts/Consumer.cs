using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Consumer : MonoBehaviour
{
    [SerializeField]
    private int[] neededResources;
    private int[] defNeededResources = new int[3];

    public Action<int[]> ResourceGained { get; set; }

    [SerializeField] private IConsumer consumer;

    private static float consumeCooldown = 0.2f;
    private float consumeTimer;

    private void Start()
    {
        consumer = GetComponent<IConsumer>();
        neededResources.CopyTo(defNeededResources, 0);

        ResourceGained?.Invoke(neededResources);
    }

    private void Update()
    {
        if(consumeTimer > 0)
        {
            consumeTimer -= Time.deltaTime;
        }
    }

    public void Restart()
    {
        defNeededResources.CopyTo(neededResources, 0);
    }

    private void Finish()
    {
        consumer?.Finish();

        if(consumer.CanRestart())
        {
            Restart();
            ResourceGained?.Invoke(neededResources);
        }
        else
        {
            ResourceGained?.Invoke(new int[] {-1});
        }
    }

    public bool DoesNeed(StackableItem.Type type)
    {
        return neededResources[(int)type] > 0 && consumeTimer <= 0;
    }

    private bool IsFinished()
    {
        foreach(int neededResource in neededResources)
        {
            if(neededResource > 0)
            {
                return false;
            }
        }

        return true;
    }

    public void ConsumeItem(StackableItem item)
    {
        if(consumeTimer <= 0)
        {
            consumeTimer = consumeCooldown;
        
            neededResources[(int)item.type]--; 

            ResourceGained?.Invoke(neededResources);
            
            if(IsFinished())
            {
                Finish();
            }
        }
    }
}

public interface IConsumer
{
    void Finish();
    bool CanRestart();
}
