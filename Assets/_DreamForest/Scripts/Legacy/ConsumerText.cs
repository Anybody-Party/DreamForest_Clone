using TMPro;
using UnityEngine;

namespace Legacy
{
    public class ConsumerText : MonoBehaviour
    {
        private TextMeshProUGUI text;
    
        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();

            ResourceConsumer resourceConsumer = transform.GetComponentInParent<ResourceConsumer>();
            resourceConsumer.ResourceGained += Consumer_ResourceGained;
        }

        private void Consumer_ResourceGained(int[] neededResources)
        {
            text.text = "";

            for(var n = 0; n < neededResources.Length; n++)
            {
                if(neededResources[n] > 0) 
                    text.text += "<sprite=" + n + ">" + neededResources[n] + "\n";
            }

            if(neededResources[0] == -1) 
                text.text = "MAX";
        }
    }
}
