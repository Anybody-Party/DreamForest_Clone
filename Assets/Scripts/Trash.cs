using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private Consumer consumer;

    private void Start()
    {
        consumer = GetComponent<Consumer>();
        consumer.ResourceGained += Consumer_ResourceGained;
    }

    private void Consumer_ResourceGained(int[] neededResources)
    {
        consumer.Restart();
    }
}
