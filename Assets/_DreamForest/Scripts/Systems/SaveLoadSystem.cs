using _DreamForest.GameServices;
using RH.Utilities.PseudoEcs;
using RH.Utilities.Saving;
using RH.Utilities.ServiceLocator;

namespace _DreamForest.Systems
{
    public class SaveLoadSystem : BaseInitSystem
    {
        private readonly DataService _dataService;

        public SaveLoadSystem()
        {
            _dataService = Services.Single<DataService>();
        }

        public override void Init()
        {
            LoadIfExist();
        }

        public override void Dispose()
        {
            Save();
        }

        private void Save() => 
            _dataService.SavableData.Save();

        private void LoadIfExist() => 
            _dataService.SavableData.LoadIfExist();
    }
}