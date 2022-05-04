using _DreamForest.GameServices;
using _DreamForest.Legacy;
using _Game.Data;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.LevelObjects
{
    public class ExchangeResourcesObject : MonoBehaviour, IConsumer
    {
        [SerializeField] private ResourceType _type;
        [SerializeField] private int _exchangeRate;

        private WalletService _wallet;

        private int _resourceBuffer;

        private void Start() => 
            _wallet = Services.Single<WalletService>();

        public void PerformOnFilled()
        {
            _resourceBuffer++;

            if (_resourceBuffer > _exchangeRate)
            {
                _resourceBuffer = 0;
                _wallet.Add(1, _type);
            }
        }

        public void PerformOnReceiveResource()
        {
            //should not be called
        }
    }
}