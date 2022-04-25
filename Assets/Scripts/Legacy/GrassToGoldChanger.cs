using UnityEngine;

namespace Legacy
{
    public class GrassToGoldChanger : MonoBehaviour, IConsumer
    {
        public void PerformOnReceiveResource(PlayerStacking from) => 
            @from.AddItem(StackableItem.Type.Gold, transform.position);

        public void PerformOnFilled()
        {
            //should not be called
        }
    }
}