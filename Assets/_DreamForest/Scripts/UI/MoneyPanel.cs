using _DreamForest.GameServices;
using _Game.Data;
using RH.Utilities.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace _DreamForest.UI
{
    public class MoneyPanel : MonoBehaviour
    {
        [SerializeField] private Text _value;

        private IWalletService _wallet;
        private GlobalEventsService _globalEvents;

        private void Start()
        {
            _wallet = Services.Instance.Single<IWalletService>();
            _globalEvents = Services.Instance.Single<GlobalEventsService>();

            _globalEvents.MoneyCountChanged.AddListener(UpdateText);

            UpdateText();
        }

        private void OnDestroy() => 
            _globalEvents.MoneyCountChanged.RemoveListener(UpdateText);

        private void UpdateText(IWalletService arg0) => UpdateText();

        private void UpdateText() => 
            _value.text = _wallet.Money.ToString();
    }
}
