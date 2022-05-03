using System.Linq;
using _DreamForest.GameServices;
using RH.Utilities.PseudoEcs;
using RH.Utilities.ServiceLocator;
using UnityEngine.SceneManagement;

namespace _DreamForest.Systems
{
    public class LoadLevelSystem : IInitSystem
    {
        private readonly DataService _data;
        private readonly BootstrapConfigsService _bootstrapConfigs;

        public LoadLevelSystem()
        {
            _data = Services.Single<DataService>();
            _bootstrapConfigs = Services.Single<BootstrapConfigsService>();
        }

        public void Init() =>
            SceneManager.LoadScene(
                _bootstrapConfigs.ScenesPerLevels
                    .First(x => x.Index == _data.SavableData.SceneIndex).Index);
    }
}