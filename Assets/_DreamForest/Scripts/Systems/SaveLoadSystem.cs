using _Game.Data;
using RH.Utilities.PseudoEcs;
using RH.Utilities.Saving;
using RH.Utilities.ServiceLocator;

namespace _Game.Logic.Systems
{
    public class SaveLoadSystem : BaseInitSystem
    {
        private readonly DataService _dataService;

        public SaveLoadSystem()
        {
            _dataService = Services.Instance.Single<DataService>();
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