using _Game.Data;
using Legacy;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.Legacy
{
    public class GrassToGoldChanger : MonoBehaviour, IConsumer
    {
        private IWalletService _wallet;

        private void Start() => 
            _wallet = Services.Instance.Single<IWalletService>();

        public void PerformOnReceiveResource(StackableItem item, PlayerStacking from) => 
            _wallet.Add(item.ExchangeRate);

        public void PerformOnFilled()
        {
            //should not be called
        }
    }
}