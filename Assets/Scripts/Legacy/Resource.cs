using UnityEngine;

namespace Legacy
{
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
                Loot();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if(collider.gameObject.CompareTag("LawnMower"))
            {
                currentStacker = collider.GetComponentInParent<PlayerStacking>();
                currentStacker.Looting?.Invoke(true);
            }
        }

        private void OnTriggerExit(Collider collider)
        {
            if(collider.gameObject.CompareTag("LawnMower"))
            {
                if(currentStacker == collider.GetComponentInParent<PlayerStacking>())
                {
                    currentStacker.Looting?.Invoke(false);
                    currentStacker = null;
                }
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
    }
}
