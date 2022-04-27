using _DreamForest.GameServices;
using _Game.Common;
using _Game.Data;
using _Game.Logic.Systems;
using RH.Utilities.PseudoEcs;
using UnityEngine;

namespace _DreamForest.Common
{
    public class EntryPoint : AbstractEntryPoint
    {
        [SerializeField] private ConfigsService _configs;

        protected override void RegisterServices() =>
            _services
                .RegisterSingle(_configs)
                .RegisterSingle(new DataService())
                .RegisterSingle(new GlobalEventsService())
                .RegisterSingle(new WalletService());

        protected override void RegisterSystems() =>
            _systems
                .Add(new SaveLoadSystem());
    }
}