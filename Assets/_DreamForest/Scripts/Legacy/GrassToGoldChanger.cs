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

        public void PerformOnReceiveResource(PlayerStacking from) => 
            _wallet.Add(1);

        public void PerformOnFilled()
        {
            //should not be called
        }
    }
}