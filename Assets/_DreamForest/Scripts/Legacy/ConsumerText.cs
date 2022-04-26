using TMPro;
using UnityEngine;

namespace Legacy
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

        private void Consumer_ResourceGained(int neededResources) => 
            text.text = "<sprite=0>" + neededResources;
    }
}
