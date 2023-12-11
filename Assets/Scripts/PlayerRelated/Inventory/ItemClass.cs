using UnityEngine;

public class ItemClass: MonoBehaviour
{
    public Item item;
    private int amount = 0;

    public void InitialiseItem(Item newItem, int amount)
    {
        this.item = newItem;
        this.GetComponent<SpriteRenderer>().sprite = newItem.sprite;
        SetAmount(amount);
    }

    public int GetPrice()
    {
        return item.price;
    }

    public string GetName()
    {
        return item.name;
    }

    public int GetAmount()
    {
        return this.amount;
    }

    public void AddAmount(int extraAmount)
    {
        this.amount = this.amount + extraAmount;
    }

    public void SetAmount(int newAmount)
    {
        this.amount = newAmount;
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }
}
