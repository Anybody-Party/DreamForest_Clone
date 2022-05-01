using _DreamForest.Legacy;
using _Game.Data;
using RH.Utilities.ServiceLocator;
using RH.Utilities.UI;
using UnityEngine;

namespace _DreamForest.Debug
{
    public class AddResourceButton : BaseActionButton
    {
        [SerializeField] private ResourceType _type;

        protected override void PerformOnClick() => 
            Services.Single<WalletService>().Add(100, _type);
    }
}
