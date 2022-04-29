using _DreamForest.GameServices;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.LevelObjects
{
    public class SpeedBoost : MonoBehaviour
    {
        public float Time;

        private GlobalEventsService _globalEvents;

        private void Start() => 
            _globalEvents = Services.Single<GlobalEventsService>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _globalEvents.SpeedBoostRecieved.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}
