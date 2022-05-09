using UnityEngine;
public class ItemClass
{
    private int amount;
    private int damage = 0;
    private string name;
    private string tag;

    public enum ItemType
    {
        Weapon,
        Material,
        Health_Potion,
    }

    ItemType itemType;

    public ItemClass() { }

    public void Constructor(string name, int amount, int damage, string tag)
    {
        switch(tag)
        {
            case "W":
                this.amount = 1;
                this.damage = damage;
                this.name = name;
                this.tag = tag;
                this.itemType = ItemType.Weapon;
                break;
            case "M":
                this.amount = amount;
                this.name = name;
                this.tag = tag;
                this.itemType = ItemType.Material;
                break;
            case "HP":
                this.amount = amount;
                this.name = name;
                this.tag = tag;
                this.itemType = ItemType.Health_Potion;
                break;
        }
    }

    public int GetAmount()
    {
        return this.amount;
    }

    public void AddAmount(int extraAmount)
    {
        this.amount = this.amount + extraAmount;
    }

    public string GetName()
    {
        return this.name;
    }

    public int GetDamage()
    {
        return this.damage;
    }

    public string GetTag()
    {
        return this.tag;
    }

    public ItemType GetItemType()
    {
        return this.itemType;
    }

    ~ItemClass()
    {

    }
}
