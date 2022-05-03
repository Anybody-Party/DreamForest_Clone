using System;
using _DreamForest.GameServices;
using _Game.Data;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.Legacy
{
    public class ResourceObject : MonoBehaviour
    {
        public event Action Looted;

        [SerializeField] private PlayerContactTag _tag;
        [SerializeField] private ResourceType _resourceType;
        [SerializeField] private SkinType skinType;
        [SerializeField] private float _amount = 1f;

        private WalletService _wallet;
        private GlobalEventsService _globalEvents;

        private void Start()
        {
            _wallet = Services.Single<WalletService>();
            _globalEvents = Services.Single<GlobalEventsService>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.CompareTag(_tag.ToString()))
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
            _wallet.Add(_amount, _resourceType);
    }
}
