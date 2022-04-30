using _DreamForest.GameServices;
using _DreamForest.Systems;
using _Game.Data;
using RH.Utilities.PseudoEcs;
using UnityEngine;

namespace _DreamForest.Common
{
    public class EntryPoint : AbstractEntryPoint
    {
        [SerializeField] private ConfigsService _configs;
        [SerializeField] private SceneObjectsRefs _sceneObjectsRefs;

        protected override void RegisterServices() =>
            _services
                .RegisterSingle(_configs)
                .RegisterSingle(_sceneObjectsRefs)
                .RegisterSingle(new DataService())
                .RegisterSingle(new GlobalEventsService())
                .RegisterSingle(new WalletService());

        protected override void RegisterSystems() =>
            _systems
            //game logic
                .Add(new SaveLoadSystem())
                .Add(new SpeedBoostSystem())
            //visual
                .Add(new CameraOnSwitchPlayerTransitionSystem());
    }
}