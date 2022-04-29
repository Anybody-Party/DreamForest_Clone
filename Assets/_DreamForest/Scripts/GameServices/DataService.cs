using _DreamForest.Data;
using Legacy;
using RH.Utilities.ServiceLocator;

namespace _DreamForest.GameServices
{
    public class DataService : IService
    {
        public SavableData SavableData = new SavableData();
        public PlayerController PlayerController;
    }
}