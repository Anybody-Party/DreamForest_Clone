using _DreamForest.Legacy;
using _Game.Data;
using RH.Utilities.ServiceLocator;
using RH.Utilities.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _DreamForest.Debug
{
    public class AddResourceButton : BaseActionButton
    {
        [SerializeField] private int _amount = 100;
        [SerializeField] private ResourceType _type;

        protected override void PerformOnStart() => 
            GetComponentInChildren<Text>().text = "+" + _amount;

        protected override void PerformOnClick() => 
            Services
                .Single<WalletService>()
                .Add(_amount, _type);
    }
}
