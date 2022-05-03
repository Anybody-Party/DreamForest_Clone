using Cinemachine;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.GameServices
{
    public class SceneObjectsRefs : MonoBehaviour, IService
    {
        public CinemachineVirtualCamera Camera;
        public Transform[] SpawnPoints;
        public GameObject Player;
        public GameObject[] Levels;
    }
}