using UnityEngine;

namespace _DreamForest.Legacy
{
    public class Construction : MonoBehaviour, IConsumer
    {
        [SerializeField] private GameObject[] _objectsToShow;

        [Header("Do not include objects which is child of this")]
        [SerializeField] private GameObject[] _objectsToDestroy;

        public void PerformOnFilled()
        {
            foreach (GameObject objectToShow in _objectsToShow) 
                objectToShow.SetActive(true);

            foreach (GameObject objectToDestroy in _objectsToDestroy)
            {
                objectToDestroy.transform.parent = transform.parent;
                Destroy(objectToDestroy);
            }

            Destroy(gameObject);
        }

        public void PerformOnReceiveResource()
        {
            //todo: place for fx
        }

#if UNITY_EDITOR

        [ContextMenu("Hide all objects to open")]
        private void HideObjectsToOpen()
        {
            foreach (GameObject objectToShow in _objectsToShow) 
                objectToShow.SetActive(false);
        }

#endif
    }
}
