using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Sprite weaponIcon;
    [SerializeField] Sprite slimeIcon;
    [SerializeField] Sprite healthPotionIcon;


    public ItemClass itClass = new ItemClass();

    Inventory inventory;


    public void Constructor(string name, int amount, int damage, string tag)
    {
        itClass.Constructor(name,amount,damage,tag);
        switch (tag)
        {
            case "W":
                SetSprite(weaponIcon);
                break;
            case "M":
                switch(itClass.GetName())
                {
                    case "slime":
                        SetSprite(slimeIcon);
                        break;
                }
                break;
            case "HP":
                SetSprite(healthPotionIcon);
                break;
        }
    }

    void SetSprite(Sprite newSprite)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inventory = GameObject.Find("InventoryUI").GetComponent<Inventory>();
            inventory.AddItem(this,true);
        }
    }
}
