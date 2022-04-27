using System;
using _DreamForest.GameServices;
using _Game.Data;
using Legacy;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.Legacy
{
    public class ResourceObject : MonoBehaviour
    {
        public event Action Looted;

        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private SkinType skinType;

        private WalletService _wallet;
        private GlobalEventsService _globalEvents;

        private void Start()
        {
            _wallet = Services.Instance.Single<WalletService>();
            _globalEvents = Services.Instance.Single<GlobalEventsService>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag("LawnMower"))
            {
                var currentStacker = collider.GetComponentInParent<PlayerStacking>();

                if (currentStacker.HasEmptySpace())
                {
                    Loot();
                    Looted?.Invoke();
                    _globalEvents.ItemAdded?.Invoke(skinType, this);
                }
            }
        }

        private void Loot() => 
            _wallet.Add(1f, _resourceType);
    }
}
