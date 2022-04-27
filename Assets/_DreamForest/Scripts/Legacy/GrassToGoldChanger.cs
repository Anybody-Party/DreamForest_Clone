using _Game.Common;
using _Game.Data;
using Legacy;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.Legacy
{
    public class GrassToGoldChanger : MonoBehaviour, IConsumer
    {
        private WalletService _wallet;
        private ConfigsService _configs;

        private void Start()
        {
            _wallet = Services.Instance.Single<WalletService>();
            _configs = Services.Instance.Single<ConfigsService>();
        }

        public void PerformOnReceiveResource() => 
            _wallet.Add(_configs.MoneyToGrass, ResourceType.Gold);

        public void PerformOnFilled()
        {
            //should not be called
        }
    }
}