using _DreamForest.Data;
using _DreamForest.GameServices;
using _DreamForest.LevelObjects;
using RH.Utilities.PseudoEcs;
using RH.Utilities.Saving;
using RH.Utilities.ServiceLocator;

namespace _DreamForest.Systems
{
    public class GoToNextLevelSystem : BaseInitSystem
    {
        private readonly GlobalEventsService _globalEvents;
        private readonly DataService _data;
        private readonly SceneObjectsRefs _sceneObjectsRefs;

        public GoToNextLevelSystem()
        {
            _globalEvents = Services.Single<GlobalEventsService>();
            _data = Services.Single<DataService>();
            _sceneObjectsRefs = Services.Single<SceneObjectsRefs>();
        }

        public override void Init() => 
            _globalEvents.GoToNewLevelIntent.AddListener(GoToNextLevel);

        public override void Dispose() => 
            _globalEvents.GoToNewLevelIntent.RemoveListener(GoToNextLevel);

        private void GoToNextLevel()
        {
            var newIndex = _data.SavableData.Level + 1;
            newIndex %= _sceneObjectsRefs.SpawnPoints.Length;

            _data.SavableData.Clear();
            _data.SavableData = new SavableData();
            _data.SavableData.Level = newIndex;

            _globalEvents.GoToNewLevel?.Invoke();
        }
    }
}