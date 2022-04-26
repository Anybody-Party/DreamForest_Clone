using _DreamForest.GameServices;
using _Game.Data;
using Legacy;
using RH.Utilities.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace _DreamForest.UI
{
    public class GrassPanel : MonoBehaviour
    {
        [SerializeField] private Text _value;

        private GlobalEventsService _globalEvents;
        private DataService _dataService;

        private void Start()
        {
            _globalEvents = Services.Instance.Single<GlobalEventsService>();
            _dataService = Services.Instance.Single<DataService>();

            _globalEvents.StackingItemsChanged.AddListener(UpdateCount);

            UpdateText();
        }

        private void OnDestroy() => 
            _globalEvents.StackingItemsChanged.RemoveListener(UpdateCount);

        private void UpdateCount(PlayerStacking arg1) => 
            UpdateText();

        private void UpdateText() => 
            _value.text = _dataService.GrassCount.ToString();
    }
}