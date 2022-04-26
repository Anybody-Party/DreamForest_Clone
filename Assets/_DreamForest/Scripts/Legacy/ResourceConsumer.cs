using System;
using System.Linq;
using UnityEngine;

namespace Legacy
{
    public class ResourceConsumer : MonoBehaviour
    {
        public event Action<int> ResourceGained;

        public int NeededResources;
        private int defNeededResources = 0;

        private IConsumer consumer;

        private static float consumeCooldown = 0.2f;
        private float consumeTimer;

        public bool NeedResources => NeededResources > 0 && consumeTimer <= 0;

        private bool IsFinished => NeededResources == 0;

        private void Start()
        {
            consumer = GetComponent<IConsumer>();

            ResourceGained?.Invoke(NeededResources);
        }

        private void Update()
        {
            if(consumeTimer > 0)
                consumeTimer -= Time.deltaTime;
        }

        public void ConsumeItem(StackableItem item, PlayerStacking from)
        {
            ConsumeItem(item);
            consumer.PerformOnReceiveResource(item, from);
        }

        private void Finish() => 
            consumer.PerformOnFilled();

        private void ConsumeItem(StackableItem item)
        {
            if (consumeTimer > 0)
                return;

            consumeTimer = consumeCooldown;
            NeededResources--;

            ResourceGained?.Invoke(NeededResources);

            if(IsFinished)
                Finish();
        }
    }
}