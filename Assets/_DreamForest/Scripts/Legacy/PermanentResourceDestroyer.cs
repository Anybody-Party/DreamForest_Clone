using _DreamForest.GameServices;
using RH.Utilities.Attributes;
using RH.Utilities.ServiceLocator;
using UnityEngine;

namespace _DreamForest.Legacy
{
    [RequireComponent(typeof(ResourceObject))]
    public class PermanentResourceDestroyer : MonoBehaviour
    {
        [ReadOnly] public string Id;

        [SerializeField] private ResourceObject _resourceObject;
        [SerializeField] private int _healthPoints;

        private DataService _data;

        private void Start()
        {
            _data = Services.Single<DataService>();

            if (_data.SavableData.FelledTrees.Contains(Id))
            {
                Destroy();
                return;
            }

            _resourceObject.Looted += HitAndDestroyIfCan;
        }

        private void OnDestroy() => 
            _resourceObject.Looted -= HitAndDestroyIfCan;

        private void HitAndDestroyIfCan()
        {
            _healthPoints -= _data.AxeDamage;

            if (_healthPoints <= 0)
            {
                _data.SavableData.FelledTrees.Add(Id);
                Destroy();
            }
        }

        private void Destroy() => 
            Destroy(gameObject);
    }
}