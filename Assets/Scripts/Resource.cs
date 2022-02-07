using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Resource : MonoBehaviour
{
    [SerializeField] private StackableItem.Type type;

    private Animator animator;

    private static float lootDefCooldown = 0.2f;
    private float lootTimer;

    private PlayerStacking currentStacker;

    private void Start()
    {
        animator = GetComponent<Animator>();
        lootTimer = lootDefCooldown;
    }

    private void Update()
    {
        if(currentStacker != null && currentStacker.HasEmptySpace())
        {
            Loot();
        }
    }

    private void Loot()
    {
        if(lootTimer <= 0)
        {
            lootTimer = lootDefCooldown;

            animator.SetTrigger("Looting");

            StackableItem item = ItemPool.Instance.UseItem(type, transform.position);
            item.PickUp(currentStacker);
        }
        else
        {
            animator.ResetTrigger("Looting");
            lootTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            currentStacker = collider.GetComponent<PlayerStacking>();
            currentStacker.Looting?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            if(currentStacker == collider.GetComponent<PlayerStacking>())
            {
                currentStacker.Looting?.Invoke(false);
                currentStacker = null;
            }
        }
    }
}
