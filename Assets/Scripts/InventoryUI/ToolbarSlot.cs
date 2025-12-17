using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolbarSlot : ItemSlot
{
    public ToolBarSelection toolBar;

    public override void OnDrop(PointerEventData eventData)
    {
        //if there is not item already then set our item. 
        if (!Item && DragDrop.itemBeingDragged.GetComponent<EquippableItem>() != null)
        {

            DragDrop.itemBeingDragged.transform.SetParent(transform);
            DragDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);
            DragDrop.itemBeingDragged.transform.localScale = new Vector3(1, 1, 1);

            if (toolBar.GetActiveSlot().Equals(gameObject))
            {
                Debug.Log("Equipped to Active slot");
                toolBar.ActivateSlot(toolBar.activeSlot);
            }
        }
    }
}
