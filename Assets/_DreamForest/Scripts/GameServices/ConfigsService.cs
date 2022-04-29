using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.GameServices
{
    [CreateAssetMenu(fileName = "Main configs", menuName = "Game/Main configs", order = 0)]
    public class ConfigsService : ScriptableObject, IService
    {
        public float MoneyToGrass = .1f;

        [Header("PlayerController")] 
        public float Speed;
        public float BoostedSpeed;
    }
}