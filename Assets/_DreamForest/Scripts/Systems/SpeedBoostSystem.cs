using System.Collections;
using _DreamForest.GameServices;
using _DreamForest.LevelObjects;
using RH.Utilities.Coroutines;
using RH.Utilities.PseudoEcs;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.Systems
{
    public class SpeedBoostSystem : BaseInitSystem
    {
        private readonly GlobalEventsService _globalEvents;
        private readonly DataService _data;
        private readonly ConfigsService _configs;

        private Coroutine _currentBoost;

        public SpeedBoostSystem()
        {
            _globalEvents = Services.Single<GlobalEventsService>();
            _data = Services.Single<DataService>();
            _configs = Services.Single<ConfigsService>();
        }

        public override void Init() => 
            _globalEvents.SpeedBoostRecieved.AddListener(IncreaseSpeedForTime);

        public override void Dispose() => 
            _globalEvents.SpeedBoostRecieved.RemoveListener(IncreaseSpeedForTime);

        private void IncreaseSpeedForTime(SpeedBoost boost)
        {
            _data.PlayerController.ChangeSpeed(_configs.BoostedSpeed);

            if (_currentBoost != null)
                CoroutineLauncher.Stop(_currentBoost);

            _currentBoost = CoroutineLauncher.Start(RestoreSpeedAfterDelay(boost));
        }

        private IEnumerator RestoreSpeedAfterDelay(SpeedBoost speedBoost)
        {
            float time = 0f;
            
            while (time <= speedBoost.Time)
            {
                time += Time.deltaTime;
                _globalEvents.SpeedBoostTimeUpdated?.Invoke(speedBoost.Time - time, this);
                yield return null;
            }

            _globalEvents.SpeedBoostTimeUpdated?.Invoke(0f, this);

            if (_data.PlayerController != null)
                _data.PlayerController.ChangeSpeed(_configs.Speed);
        }
    }
}