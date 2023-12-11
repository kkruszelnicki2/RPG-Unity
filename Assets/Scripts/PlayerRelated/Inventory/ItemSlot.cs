using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private bool occupied = false;
    private int maxStack = 4;

    /*public InventoryItem item = InventoryItem.GetEmptyItem(); */
    public ItemClass item;

    public event Action<ItemSlot> OnItemClicked, OnRightMouseBtnClick;
    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }

    public bool isOccupied()
    {
        return occupied;
    }

    public void Occupy()
    {
        occupied = !occupied;
    }

    public void SlotUpdate(ItemClass item)
    {
        this.item = item;
        Occupy();
        transform.Find("Image").GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        Color color = transform.Find("Image").GetComponent<Image>().color;
        color.a = 1;
        transform.Find("Image").GetComponent<Image>().color = color;
        transform.Find("Amount").GetComponent<Text>().text = this.item.GetAmount().ToString();
    }

    public void AmountTextUpdate(int extraAmount)
    {
        this.item.AddAmount(extraAmount);

        if(this.item.GetAmount() <= 0)
        {
            RemoveItem();
        }
        else
        {
            transform.Find("Amount").GetComponent<Text>().text = this.item.GetAmount().ToString();
        }
    }

    public bool isFull(int extraAmount)
    {
        if(this.item.GetAmount() + extraAmount > this.maxStack)
        {
            return true;
        }
        return false;
    }

    public void RemoveItem()
    {
        if(isOccupied())
        {
            this.item = null;
            transform.Find("Image").GetComponent<Image>().sprite = null;
            transform.Find("Amount").GetComponent<Text>().text = "";
            Color color = transform.Find("Image").GetComponent<Image>().color;
            color.a = 0;
            transform.Find("Image").GetComponent<Image>().color = color;

            Occupy();
        }
    }
}
