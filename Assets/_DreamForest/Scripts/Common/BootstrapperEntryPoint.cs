using _DreamForest.GameServices;
using _DreamForest.Systems;
using RH.Utilities.PseudoEcs;
using UnityEngine;

namespace _DreamForest.Common
{
    public class BootstrapperEntryPoint : AbstractEntryPoint
    {
        [SerializeField] private BootstrapConfigsService _bootstrapConfigs;
        [SerializeField] private ConfigsService _configs;
        
        protected override void RegisterServices() =>
            _services
                .RegisterSingle(_bootstrapConfigs)
                .RegisterSingle(_configs)
                .RegisterSingle(new DataService());

        protected override void RegisterSystems() =>
            _systems
                //game logic
                .Add(new SaveLoadSystem())
                .Add(new LoadPlayerSystem());
    }
}