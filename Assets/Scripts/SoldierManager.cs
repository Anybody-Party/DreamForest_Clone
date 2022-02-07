using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoldierManager : MonoBehaviour
{
    public static SoldierManager Instance { get; private set; }

    private GameManager gameManager;

    public Action<Soldier> SoldierRecruited;
    private int[] soldierAmount = new int[2];

    private static int winCondition = 3;

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

        SoldierRecruited += _SoldierRecruited;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void _SoldierRecruited(Soldier soldier)
    {
        soldierAmount[(int)soldier.type]++;

        if(IsSoldiersFinished())
        {
            gameManager.FinishGame();
        }
    }

    private bool IsSoldiersFinished()
    {
        foreach(int amount in soldierAmount)
        {
            if(amount < winCondition)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsSoldierTypeFinished(int type)
    {
        return soldierAmount[type] >= winCondition;
    }
}
