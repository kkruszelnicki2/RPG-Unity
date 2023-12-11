using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> itemSlots = new();

    bool isVisible = false;

    Vector2 outOfGame = new(1400,450);
    Vector2 inGame = new(800,450);

    public void Awake()
    {
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.name.Equals("Item_slot"))
            {
                itemSlots.Add(child.gameObject);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I) && Time.timeScale == 1)
        {
            Move();
        }
    }

    public void AddItem(ItemClass Newitem)
    {
        foreach(GameObject itemSlot in itemSlots)
        {
            if (!itemSlot.GetComponent<ItemSlot>().isOccupied())
            {
                itemSlot.GetComponent<ItemSlot>().SlotUpdate(Newitem);
                break;
            }
            else if (itemSlot.GetComponent<ItemSlot>().item.GetName().Equals(Newitem.GetName()) && !itemSlot.GetComponent<ItemSlot>().isFull(Newitem.GetAmount()))
            {
                itemSlot.GetComponent<ItemSlot>().AmountTextUpdate(Newitem.GetAmount());
                break;
            }
        }
    }

    public void Move()
    {
        if (isVisible)
        {
            transform.position = outOfGame;
        }
        else
        {
            transform.position = inGame;
        }
        isVisible = !isVisible;
    }

    public void ResetEquipment()
    {
        foreach (GameObject itemSlot in itemSlots)
        {
            itemSlot.GetComponent<ItemSlot>().RemoveItem();
        }
    }
}
