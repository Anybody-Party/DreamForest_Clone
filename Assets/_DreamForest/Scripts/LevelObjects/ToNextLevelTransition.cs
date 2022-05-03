using _DreamForest.GameServices;
using _DreamForest.Legacy;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.LevelObjects
{
    public class ToNextLevelTransition : MonoBehaviour, IConsumer
    {
        private GlobalEventsService _globalEvents;

        private void Start() =>
            _globalEvents = Services.Single<GlobalEventsService>();

        public void PerformOnFilled() =>
            _globalEvents.GoToNewLevelIntent.Invoke();

        public void PerformOnReceiveResource()
        {
            //empty
        }
    }
}