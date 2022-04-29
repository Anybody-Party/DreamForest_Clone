using _DreamForest.GameServices;
using _DreamForest.Systems;
using RH.Utilities.Extensions;
using RH.Utilities.ServiceLocator;
using UnityEngine;
using UnityEngine.UI;

namespace _DreamForest.UI
{
    public class SpeedBoostTimerPanel : MonoBehaviour
    {
        [SerializeField] private Text _label;
        
        private GlobalEventsService _globalEvents;

        private void Awake()
        {
            _globalEvents = Services.Single<GlobalEventsService>();
            _globalEvents.SpeedBoostTimeUpdated.AddListener(EnableAndShowTime);

            gameObject.SetActive(false);
        }

        private void OnDestroy() => 
            _globalEvents.SpeedBoostTimeUpdated.RemoveListener(EnableAndShowTime);

        private void EnableAndShowTime(float time, SpeedBoostSystem speedBoostSystem)
        {
            if (!gameObject.activeSelf)
                gameObject.SetActive(true);

            _label.text = time.ToString("F2");

            if (time.Approximately(0f))
                gameObject.SetActive(false);
        }
    }
}