using _DreamForest.Data;
using Legacy;
using RH.Utilities.ServiceLocator;

namespace _DreamForest.GameServices
{
    public class DataService : IService
    {
        public SavableData SavableData = new SavableData();
        public PlayerController PlayerController;
        
        private readonly ConfigsService _configs;

        public int AxeDamage => _configs.AxeDamages[SavableData.AxeLevel];

        public DataService()
        {
            _configs = Services.Single<ConfigsService>();
        }
    }
}