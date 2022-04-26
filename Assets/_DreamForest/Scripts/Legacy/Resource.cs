using System;
using UnityEngine;

namespace Legacy
{
    public class Resource : MonoBehaviour
    {
        public event Action Looted;

        [SerializeField] private StackableItem.Type type;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("LawnMower"))
            {
                var currentStacker = collider.GetComponentInParent<PlayerStacking>();

                if (currentStacker.HasEmptySpace())
                {
                    Loot(currentStacker);
                    Looted?.Invoke();
                }
            }
        }

        private void Loot(PlayerStacking currentStacker)
        {
            StackableItem item = ItemPool.Instance.UseItem(type, transform.position);
            item.PickUp(currentStacker);
        }
    }
}
