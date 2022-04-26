using System;
using System.Linq;
using UnityEngine;

namespace Legacy
{
    public class ResourceConsumer : MonoBehaviour
    {
        public event Action<int[]> ResourceGained;

        [SerializeField] private int[] neededResources;
        private int[] defNeededResources = new int[Enum.GetNames(typeof(StackableItem.Type)).Length];

        private IConsumer consumer;

        private static float consumeCooldown = 0.2f;
        private float consumeTimer;

        private void Start()
        {
            consumer = GetComponent<IConsumer>();
            neededResources.CopyTo(defNeededResources, 0);

            ResourceGained?.Invoke(neededResources);
        }

        private void Update()
        {
            if(consumeTimer > 0)
                consumeTimer -= Time.deltaTime;
        }

        private void Finish() => 
            consumer.PerformOnFilled();

        public bool DoesNeed(StackableItem.Type type) => 
            neededResources[(int)type] > 0 && consumeTimer <= 0;

        private bool IsFinished() => 
            neededResources.All(neededResource => neededResource <= 0);

        public void ConsumeItem(StackableItem item, PlayerStacking from)
        {
            ConsumeItem(item);
            consumer.PerformOnReceiveResource(from);
        }

        private void ConsumeItem(StackableItem item)
        {
            if (consumeTimer > 0)
                return;

            consumeTimer = consumeCooldown;
            neededResources[(int)item.type]--;

            ResourceGained?.Invoke(neededResources);

            if(IsFinished()) 
                Finish();
        }
    }
}