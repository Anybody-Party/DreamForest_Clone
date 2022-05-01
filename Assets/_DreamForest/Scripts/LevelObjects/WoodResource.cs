using System;
using System.Collections;
using _DreamForest.GameServices;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.LevelObjects
{
    public class WoodResource : MonoBehaviour
    {
        [SerializeField] private int _woodCount;
        [SerializeField] private int _hitPoints;
        [SerializeField] private float _cooldownTime;

        private bool _inCooldown;
        private static WaitForSeconds _waitForCooldown;
        private DataService _data;

        private void Start()
        {
            _data = Services.Single<DataService>();
            _waitForCooldown ??= new WaitForSeconds(_cooldownTime);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && !_inCooldown)
            {
                TakeHit();
                StartCoroutine(GoInCooldown());
            }
        }

        private void TakeHit()
        {
            var receivedDamage = _data.AxeDamage;
        }

        private IEnumerator GoInCooldown()
        {
            _inCooldown = true;
            yield return _waitForCooldown;
        }
    }
}
