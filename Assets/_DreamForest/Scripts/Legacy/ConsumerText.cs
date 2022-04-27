using _DreamForest.Data;
using Legacy;
using TMPro;
using UnityEngine;

namespace _DreamForest.Legacy
{
    public class ConsumerText : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private ResourceConsumer _resourceConsumer;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();

            _resourceConsumer = transform.GetComponentInParent<ResourceConsumer>();
            _resourceConsumer.ResourceGained += Consumer_ResourceGained;

            Consumer_ResourceGained(_resourceConsumer.NeededResources);
        }

        private void OnDestroy() => 
            _resourceConsumer.ResourceGained -= Consumer_ResourceGained;

        private void Consumer_ResourceGained(Resource[] neededResources)
        {
            text.text = string.Empty;

            foreach (Resource resource in neededResources)
                text.text = $"<sprite={(int) resource.Type}>{resource.Amount}";
        }
    }
}
