using _DreamForest.Legacy;
using _Game.Data;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.LevelObjects
{
    public class BuildingWithGold : MonoBehaviour
    {
        [SerializeField] private float _amount;
        [SerializeField] private ResourceType _type;

        private WalletService _wallet;

        private void Start()
        {
            _wallet = Services.Single<WalletService>();
            _wallet.Add(_amount, _type);
            Destroy(this);
        }
    }
}