using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RecruiterBuilding : MonoBehaviour, IConsumer
{
    [SerializeField] private GameObject soldierPrefab;

    private Vector3 spawnPoint;

    private bool isFinished;

    private void Start()
    {
        spawnPoint = transform.Find("SpawnPoint").position;
    }

    public void Finish()
    {
        Soldier soldier = Instantiate(soldierPrefab, spawnPoint, Quaternion.identity).GetComponent<Soldier>();

        SoldierManager.Instance.SoldierRecruited?.Invoke(soldier);
        
        if(SoldierManager.Instance.IsSoldierTypeFinished((int)soldier.type))
        {
            isFinished = true;
        }
    }

    public bool CanRestart()
    {
        return !isFinished;
    }
}
