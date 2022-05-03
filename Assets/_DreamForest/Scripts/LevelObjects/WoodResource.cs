using System.Collections;
using _DreamForest.GameServices;
using _DreamForest.Legacy;
using _Game.Data;
using RH.Utilities.Attributes;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.LevelObjects
{
    public class WoodResource : MonoBehaviour
    {
        [ReadOnly] public string Id;

        [SerializeField] private int _originWood;
        [SerializeField] private int _originHealth;
        [SerializeField] private float _cooldownTime;

        [SerializeField, ReadOnly] private int _currentWood;
        [SerializeField, ReadOnly] private int _currentHealth;
        [SerializeField, ReadOnly] private bool _inCooldown;

        private static WaitForSeconds _waitForCooldown;

        private DataService _data;
        private WalletService _wallet;

        private void Start()
        {
            _waitForCooldown ??= new WaitForSeconds(_cooldownTime);
            _data = Services.Single<DataService>();
            _wallet = Services.Single<WalletService>();

            if (_data.SavableData.FelledTrees.Contains(Id))
                Destroy();

            _currentHealth = _originHealth;
            _currentWood = _originWood;
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
            _currentHealth -= receivedDamage;
            int restWood = (int)Mathf.Max(0f, (float)_currentHealth / _originHealth * _originWood);

            if (restWood < _currentWood)
                AddWood(restWood);

            if (_currentHealth <= 0)
            {
                _data.SavableData.FelledTrees.Add(Id);
                Destroy();
            }
        }

        private void AddWood(int restWood)
        {
            _wallet.Add(_currentWood - restWood, ResourceType.Wood);
            _currentWood = restWood;
        }

        private IEnumerator GoInCooldown()
        {
            _inCooldown = true;
            yield return _waitForCooldown;
            _inCooldown = false;
        }

        private void Destroy()
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}
