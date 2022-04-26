using System.Collections;
using UnityEngine;

namespace Legacy
{
    [RequireComponent(typeof(Resource))]
    public class ResourceRestorer : MonoBehaviour
    {
        [SerializeField] private Resource _resource;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _gfx;

        [SerializeField] private float _restoreTime;

        private void Start() => 
            _resource.Looted += HideForWhile;

        private void OnDestroy() => 
            _resource.Looted -= HideForWhile;

        private void HideForWhile()
        {
            _collider.enabled = false;
            _gfx.SetActive(false);

            StartCoroutine(RestoreDelayed());
        }

        private IEnumerator RestoreDelayed()
        {
            yield return new WaitForSeconds(_restoreTime);

            _collider.enabled = true;
            _gfx.SetActive(true);
        }
    }
}
