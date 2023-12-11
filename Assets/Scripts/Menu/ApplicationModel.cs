using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemSlot;

public static class ApplicationModel
{
    static public int isLoaded = 2; // 0 - NEW GAME | 1 - LOAD GAME | EVERYTHING ELSE - TRAVELING THROUGH LOCATIONS

    // PLAYER ITEMS
    public static List<int> itemAmount = new List<int>();
    public static List<ItemClass> itemSO = new List<ItemClass>();

    // PLAYER DATA
    public static Vector3 playerPos;
    public static int playerLevel;
    public static int playerExp;
    public static int playerMaxExp;
    public static int playerHp;
    public static int playerMaxHp;

    // LOCATION
    public static string locationName = "Map1";

    public static void saveData(Portal.SaveData saveData)
    {
        ResetData();
        foreach (GameObject itemSlot in GameObject.Find("InventoryUI").GetComponent<Inventory>().itemSlots)
        {
            if (itemSlot.GetComponent<ItemSlot>().isOccupied())
            {
                itemAmount.Add(itemSlot.GetComponent<ItemSlot>().item.GetAmount());
                itemSO.Add(itemSlot.GetComponent<ItemSlot>().item);
            }
        }

        //Player
        playerPos = saveData.playePos;
        playerLevel = saveData.playerLevel;
        playerExp = saveData.playerExp;
        playerMaxExp = saveData.playerMaxExp;
        playerHp = saveData.playerHp;
        playerMaxHp = saveData.playerMaxHp;

        //Location
        locationName = saveData.locationName;
    }

    private static void ResetData()
    {
        itemAmount.Clear();
        itemSO.Clear();
    }
}
