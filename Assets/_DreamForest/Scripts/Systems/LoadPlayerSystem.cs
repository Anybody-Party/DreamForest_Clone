using _DreamForest.GameServices;
using RH.Utilities.PseudoEcs;
using RH.Utilities.ServiceLocator;
using Object = UnityEngine.Object;

namespace _DreamForest.Systems
{
    public class RemoveCompletedLevelsSystem : BaseInitSystem
    {
        private readonly DataService _data;
        private readonly SceneObjectsRefs _sceneObjects;
        private readonly GlobalEventsService _globalEvents;

        public RemoveCompletedLevelsSystem()
        {
            _data = Services.Single<DataService>();
            _sceneObjects = Services.Single<SceneObjectsRefs>();
            _globalEvents = Services.Single<GlobalEventsService>();
        }

        public override void Init()
        {
            ClearOldLevels();

            _globalEvents.GoToNewLevel.AddListener(ClearOldLevels);
        }

        public override void Dispose() => 
            _globalEvents.GoToNewLevel.RemoveListener(ClearOldLevels);

        private void ClearOldLevels()
        {
            for (int i = 0; i < _sceneObjects.Levels.Length; i++)
            {
                if (_sceneObjects.Levels[i] != null && i < _data.SavableData.Level)
                    Object.Destroy(_sceneObjects.Levels[i]);
            }
        }
    }

    
    public class LoadPlayerSystem : BaseInitSystem
    {
        private readonly DataService _data;
        private readonly SceneObjectsRefs _sceneObjects;
        private readonly GlobalEventsService _globalEvents;

        public LoadPlayerSystem()
        {
            _data = Services.Single<DataService>();
            _sceneObjects = Services.Single<SceneObjectsRefs>();
            _globalEvents = Services.Single<GlobalEventsService>();
        }

        public override void Init()
        {
            SetPlayerPosition();

            _globalEvents.GoToNewLevel.AddListener(SetPlayerPosition);
        }

        public override void Dispose() => 
            _globalEvents.GoToNewLevel.RemoveListener(SetPlayerPosition);

        private void SetPlayerPosition() => 
            _sceneObjects.Player.transform.position = _sceneObjects.SpawnPoints[_data.SavableData.Level].position;
    }
}