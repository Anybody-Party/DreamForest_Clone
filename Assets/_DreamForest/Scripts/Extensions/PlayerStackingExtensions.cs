using _DreamForest.Legacy;
using Legacy;
using UnityEngine;

public static class PlayerStackingExtensions
{
    public static void AddItem(this PlayerStacking stacking, SkinType type, Vector3 position)
    {
        StackableItem item = ItemPool.Instance.UseItem(type, position);
        item.PickUp(stacking);
    }
}