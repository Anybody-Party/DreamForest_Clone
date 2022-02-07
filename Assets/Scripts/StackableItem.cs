using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableItem : MonoBehaviour
{
    public enum Type
    {
        Wood,
        Stone,
        Gold
    }

    public Type type;

    private Transform previousStacked;
    [SerializeField] private float spaceY = 1;

    private float stackSpeed = 25;

    private void Update()
    {
        if(previousStacked != null && transform.parent != previousStacked)
        {
            UpdatePosition();
        }
    }

    private void UpdatePosition()
    {
        Vector3 targetPosition = previousStacked.position + previousStacked.up * spaceY;
        transform.forward = previousStacked.forward;

        if(transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, stackSpeed * Time.deltaTime);
        }
        else if(transform.parent != previousStacked)
        {
            transform.SetParent(previousStacked, true);
        }
    }

    public void PickUp(PlayerStacking stacker)
    {
        if(!stacker.HasEmptySpace()) return;

            previousStacked = stacker.GetLastItem().transform;
            stacker.AddItem(this);

            transform.parent = null;
    }

    public void Poolize()
    {
        previousStacked = null;
        
        ItemPool.Instance.PoolizeItem(this);
    }
}
