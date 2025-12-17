using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquippableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public GameObject EquipPrefab;
    public GameObject EquipObject;
    public ToolBarSelection ToolBar;
    public DragDrop dragDrop;

    void Awake()
    {
        if(ToolBar == null) ToolBar = GameObject.Find("ToolBar").GetComponent<ToolBarSelection>();
        if(dragDrop == null) dragDrop = GetComponent<DragDrop>();
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(dragDrop.startParent.Equals(ToolBar.GetActiveSlot().transform))
            ToolBar.Unequip(this);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragDrop.startParent.Equals(ToolBar.GetActiveSlot().transform) && transform.parent == dragDrop.startParent || transform.parent == transform.root)
        {
          ToolBar.Equip(this);
        }

    }
}
