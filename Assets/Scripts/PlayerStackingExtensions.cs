using Legacy;
using UnityEngine;

public static class PlayerStackingExtensions
{
    public static void AddItem(this PlayerStacking stacking, StackableItem.Type type, Vector3 position)
    {
        StackableItem item = ItemPool.Instance.UseItem(type, position);
        item.PickUp(stacking);
    }
}