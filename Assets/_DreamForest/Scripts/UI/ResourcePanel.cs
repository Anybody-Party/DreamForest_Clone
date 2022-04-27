using _DreamForest.GameServices;
using _DreamForest.Legacy;
using _Game.Data;
using Legacy;
using RH.Utilities.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace _DreamForest.UI
{
    public class ResourcePanel : MonoBehaviour
    {
        [SerializeField] private Text _valueTitle;
        [SerializeField] private ResourceType _targetType;

        private WalletService _wallet;
        private GlobalEventsService _globalEvents;

        private void Start()
        {
            _wallet = Services.Instance.Single<WalletService>();
            _globalEvents = Services.Instance.Single<GlobalEventsService>();

            _globalEvents.ResourcesCountChanged.AddListener(UpdateText);

            UpdateText();
        }

        private void UpdateText(ResourceType type, WalletService arg1)
        {
            if (type == _targetType)
                UpdateText();
        }

        private void OnDestroy() => 
            _globalEvents.ResourcesCountChanged.RemoveListener(UpdateText);

        private void UpdateText() => 
            _valueTitle.text = _wallet.GetAmount(_targetType).ToString("F0");
    }
}
