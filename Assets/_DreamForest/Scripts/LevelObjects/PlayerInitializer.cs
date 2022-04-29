using _DreamForest.GameServices;
using Legacy;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.LevelObjects
{
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInitializer : MonoBehaviour
    {
        private void Start()
        {
            Services.Single<DataService>().PlayerController = GetComponent<PlayerController>();
            Destroy(this);
        }
    }
}