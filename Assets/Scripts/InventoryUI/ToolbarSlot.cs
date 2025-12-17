using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolbarSlot : ItemSlot
{
    public override void OnDrop(PointerEventData eventData)
    {
        //if there is not item already then set our item. 
        if (!Item && DragDrop.itemBeingDragged.GetComponent<DragDrop>().equippable)
        {

            DragDrop.itemBeingDragged.transform.SetParent(transform);
            DragDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);
            DragDrop.itemBeingDragged.transform.localScale = new Vector3(1, 1, 1);

        }
    }
}
