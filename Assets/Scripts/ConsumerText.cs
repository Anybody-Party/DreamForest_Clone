using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsumerText : MonoBehaviour
{
    private TextMeshProUGUI text;
    
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        Consumer consumer = transform.parent.parent.parent.GetComponent<Consumer>();
        consumer.ResourceGained += Consumer_ResourceGained;
    }

    private void Consumer_ResourceGained(int[] neededResources)
    {
        text.text = "";

        for(var n = 0; n < neededResources.Length; n++)
        {
            if(neededResources[n] > 0)
            {
                text.text += "<sprite="+n+">" + neededResources[n] + "\n";
            }
        }

        if(neededResources[0] == -1)
        {
            text.text = "MAX";
        }
    }
}
