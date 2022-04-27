using _DreamForest.Data;
using RH.Utilities.ServiceLocator;

namespace _Game.Data
{
    public class DataService : IService
    {
        public SavableData SavableData = new SavableData();
    }
}