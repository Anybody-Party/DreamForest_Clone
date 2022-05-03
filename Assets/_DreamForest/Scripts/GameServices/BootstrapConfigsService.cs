using System;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.GameServices
{
    [CreateAssetMenu(fileName = "Main configs", menuName = "Game/Bootstrap configs", order = 0)]
    public class BootstrapConfigsService : ScriptableObject, IService
    {
        public SceneToLevel[] ScenesPerLevels;

        [Serializable]
        public class SceneToLevel
        {
            public int Level;
            public int Index;
        }
    }
}