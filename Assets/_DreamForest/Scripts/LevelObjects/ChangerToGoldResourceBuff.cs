using UnityEngine;

namespace _DreamForest.LevelObjects
{
    public class ChangerToGoldResourceBuff : MonoBehaviour
    {
        // private GlobalEventsService _globalEvents;
        //
        // private void Start() =>
        //     _globalEvents = Services.Single<GlobalEventsService>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                CreateResourceChanger();
            }
        }

        private void CreateResourceChanger()
        {
            
        }
    }
}