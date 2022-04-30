using _DreamForest.GameServices;
using _DreamForest.LevelObjects;
using RH.Utilities.PseudoEcs;
using RH.Utilities.ServiceLocator;
using TMPro;
using UnityEngine;

namespace _DreamForest.Systems
{
    public class CameraOnSwitchPlayerTransitionSystem : BaseInitSystem
    {
        private readonly GlobalEventsService _globalEvents;
        private readonly SceneObjectsRefs _sceneObjects;

        private Vector3 _offset;

        public CameraOnSwitchPlayerTransitionSystem()
        {
            _globalEvents = Services.Single<GlobalEventsService>();
            _sceneObjects = Services.Single<SceneObjectsRefs>();
        }

        public override void Init() => 
            _globalEvents.PlayerChanged.AddListener(MoveCameraToNewPlayer);

        public override void Dispose() => 
            _globalEvents.PlayerChanged.RemoveListener(MoveCameraToNewPlayer);

        private void MoveCameraToNewPlayer(GameObject targetPlayer, PlayerTransitionZone arg1)
        {
            Transform playerTransform = targetPlayer.transform;

            _sceneObjects.Camera.Follow = playerTransform;
            _sceneObjects.Camera.LookAt = playerTransform;
        }
    }
}