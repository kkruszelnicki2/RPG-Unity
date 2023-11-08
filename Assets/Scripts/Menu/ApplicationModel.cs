using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ApplicationModel
{
    static public int isLoaded = 2; // 0 - NEW GAME | 1 - LOAD GAME | EVERYTHING ELSE - TRAVELING THROUGH LOCATIONS

    // PLAYER ITEMS
    public static List<int> itemAmount = new List<int>();
    public static List<int> itemDamage = new List<int>();
    public static List<string> itemTag = new List<string>();
    public static List<string> itemName = new List<string>();

    // PLAYER DATA
    public static Vector3 playerPos;

    // LOCATION
    public static string locationName = "Map1";

    public static void saveData(Portal.SaveData saveData)
    {
        foreach (GameObject itemSlot in GameObject.Find("InventoryUI").GetComponent<Inventory>().itemSlots)
        {
            if (itemSlot.GetComponent<ItemSlot>().isOccupied())
            {
                itemAmount.Add(itemSlot.GetComponent<ItemSlot>().item.GetAmount());
                itemDamage.Add(itemSlot.GetComponent<ItemSlot>().item.GetDamage());
                itemTag.Add(itemSlot.GetComponent<ItemSlot>().item.GetTag());
                itemName.Add(itemSlot.GetComponent<ItemSlot>().item.GetName());
            }
        }

        //Player
        playerPos = saveData.playePos;

        //Location
        locationName = saveData.locationName;
    }
}
