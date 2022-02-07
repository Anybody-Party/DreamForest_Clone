using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Construction : MonoBehaviour, IConsumer
{
    [SerializeField] private GameObject building;

    public void Finish()
    {
        building.transform.parent = null;
        building.SetActive(true);

        Destroy(gameObject);
    }

    public bool CanRestart()
    {
        return false;
    }
}
