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

        private void Start()
        {
            _wallet = Services.Instance.Single<IWalletService>();

            _wallet.MoneyCountChanged += UpdateText;

            UpdateText();
        }

        private void OnDestroy() => 
            _wallet.MoneyCountChanged -= UpdateText;

        private void UpdateText() => 
            _value.text = _wallet.Money.ToString();
    }
}
