using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        PlayerController playerController = GetComponent<PlayerController>();
        playerController.Walking = PlayerController_Walking;

        PlayerStacking playerStacking = GetComponent<PlayerStacking>();
        playerStacking.Looting = PlayerStacking_Looting;
    }

    private void PlayerController_Walking(float playerSpeed)
    {
        animator.SetBool("isWalking", playerSpeed > 0);
        animator.SetFloat("WalkingSpeed", playerSpeed);
    }

    private void PlayerStacking_Looting(bool isLooting)
    {
        animator.SetBool("isLooting", isLooting);
    }
}
