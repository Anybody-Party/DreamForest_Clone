using _DreamForest.GameServices;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.LevelObjects
{
    public class PlayerTransitionZone : MonoBehaviour
    {
        [SerializeField] private GameObject _playerToShow;
        [SerializeField] private Transform _oldPlayerMovePosition;

        private GlobalEventsService _globalEvents;

        private void Start() => 
            _globalEvents = Services.Single<GlobalEventsService>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.SetActive(false);
                other.transform.position = _oldPlayerMovePosition.position;

                _playerToShow.SetActive(true);

                _globalEvents.PlayerChanged?.Invoke(_playerToShow, this);
            }
        }
    }
}