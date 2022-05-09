using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Sprite weaponIcon;
    [SerializeField] Sprite slimeIcon;
    [SerializeField] Sprite healthPotionIcon;

    private int id;
    private bool occupied = false;
    private int maxStack = 4;

    public ItemClass item;

    public int GetID()
    {
        return this.id;
    }

    public void SetID(int newID)
    {
        this.id = newID;
    }

    public bool isOccupied()
    {
        return occupied;
    }

    public void Occupy()
    {
        occupied = !occupied;
    }

    public void SlotUpdate(Item item)
    {
        this.item = new ItemClass();
        Occupy();
        this.item.Constructor(item.GetComponent<Item>().itClass.GetName(), item.GetComponent<Item>().itClass.GetAmount(), item.GetComponent<Item>().itClass.GetDamage(), item.GetComponent<Item>().itClass.GetTag());
        transform.Find("Image").GetComponent<Image>().sprite = item.GetComponent<SpriteRenderer>().sprite;
        Color color = transform.Find("Image").GetComponent<Image>().color;
        color.a = 1;
        transform.Find("Image").GetComponent<Image>().color = color;

        transform.Find("Amount").GetComponent<Text>().text = this.item.GetAmount().ToString();
    }

    public void AmountTextUpdate(int extraAmount)
    {
        this.item.AddAmount(extraAmount);
        transform.Find("Amount").GetComponent<Text>().text = this.item.GetAmount().ToString();
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
