using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemEventHandler : MonoBehaviour
{
    private GameObject player;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Start()
    {
        transform.parent.GetComponent<ItemSlot>().OnItemClicked += ItemClickHandler;
        transform.parent.GetComponent<ItemSlot>().OnRightMouseBtnClick += ItemRightClickHandler;
    }

    private void ItemClickHandler(ItemSlot obj)
    {
        if (obj.GetComponent<ItemSlot>().isOccupied())
        {
            IItemAction itemAction = obj.GetComponent<ItemSlot>().item.item as IItemAction;

            if (itemAction != null)
            {
                itemAction.PerformAction(player);

                if(itemAction.ActionName != "Nothing")
                {
                    obj.GetComponent<ItemSlot>().AmountTextUpdate(-1);
                }
            }
        }
    }

    private void ItemRightClickHandler(ItemSlot obj)
    {
        if(obj.GetComponent<ItemSlot>().isOccupied())
        {
            obj.GetComponent<ItemSlot>().RemoveItem();
        }
    }
}
