using Legacy;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        PlayerController playerController = GetComponent<PlayerController>();
        playerController.Walking = PlayerController_Walking;
    }

    private void PlayerController_Walking(float playerSpeed)
    {
        animator.SetBool("isWalking", playerSpeed > 0);
        animator.SetFloat("WalkingSpeed", playerSpeed);
    }
}
