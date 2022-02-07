using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    private Animator animator;

    public enum Type
    {
        Spearman,
        Archer
    }

    public Type type;

    private void Start()
    {
        animator = GetComponent<Animator>();

        SoldierManager.Instance.SoldierRecruited += SoldierManager_SoldierRecruited;
    }

    private void SoldierManager_SoldierRecruited(Soldier soldier)
    {
        if(soldier.type == type && soldier != this)
        {
            animator.SetTrigger("Celebrate");
        }
    }
}
