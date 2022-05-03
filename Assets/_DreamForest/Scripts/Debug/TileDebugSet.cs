using UnityEngine;

namespace _DreamForest.Debug
{
    [ExecuteInEditMode]
    public class TileDebugSet : MonoBehaviour
    {
        private void Start() => Set();

        private void Set()
        {
            transform.localScale = new Vector3(0.438179612f, 0.373809606f, 0.452945083f);
            transform.localPosition = new Vector3(0, 0.0900000036f, -0.0149999997f);

            if (transform.parent.TryGetComponent(out MeshFilter meshFilter))
            {
                DestroyImmediate(meshFilter);
                DestroyImmediate(transform.parent.GetComponent<MeshRenderer>());
            }

            DestroyImmediate(this);
        }
    }
}
