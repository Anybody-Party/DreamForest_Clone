using UnityEngine;

namespace Legacy
{
    public class Construction : MonoBehaviour, IConsumer
    {
        [SerializeField] private GameObject building;

        public void PerformOnFilled()
        {
            building.transform.parent = transform.parent;
            building.SetActive(true);

            Destroy(gameObject);
        }

        public void PerformOnReceiveResource(PlayerStacking @from)
        {
            //todo: place for fx
        }
    }
}
