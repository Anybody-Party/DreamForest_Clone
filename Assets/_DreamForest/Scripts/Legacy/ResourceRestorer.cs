using System.Collections;
using _DreamForest.Legacy;
using UnityEngine;

namespace Legacy
{
    [RequireComponent(typeof(ResourceObject))]
    public class ResourceRestorer : MonoBehaviour
    {
        [SerializeField] private ResourceObject _resourceObject;
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _gfx;

        [SerializeField] private float _restoreTime;

        private void Start() => 
            _resourceObject.Looted += HideForWhile;

        private void OnDestroy() => 
            _resourceObject.Looted -= HideForWhile;

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
