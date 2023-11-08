using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> itemSlots = new List<GameObject>();

    bool isVisible = false;

    Vector2 outOfGame = new Vector2(1400,450);
    Vector2 inGame = new Vector2(800,450);

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I) && Time.timeScale == 1)
        {
            Move();
        }
    }
    public void Constructor()
    {
        int i = 1;
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if (child.name.Equals("Item_slot"))
            {
                itemSlots.Add(child.gameObject);
                child.GetComponent<ItemSlot>().SetID(i);
                i++;
            }
            
        }
    }

    public void AddItem(Item Newitem, bool isPicked)
    {
        foreach(GameObject itemSlot in itemSlots)
        {
            if (!itemSlot.GetComponent<ItemSlot>().isOccupied())
            {
                itemSlot.GetComponent<ItemSlot>().SlotUpdate(Newitem);
                if (isPicked)
                {
                    Destroy(Newitem.gameObject);
                }
                break;
            }
            else if (itemSlot.GetComponent<ItemSlot>().item.GetName().Equals(Newitem.itClass.GetName()) && !itemSlot.GetComponent<ItemSlot>().isFull(Newitem.itClass.GetAmount()))
            {
                itemSlot.GetComponent<ItemSlot>().AmountTextUpdate(Newitem.itClass.GetAmount());
                if (isPicked)
                {
                    Destroy(Newitem.gameObject);
                }
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
