using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDrop : MonoBehaviour
{
    private int slimeChance = 50;
    private int potionChance = 50;
    public void Drop(GameObject item)
    {
        int chance = Random.Range(1, 100);
        if (chance <= slimeChance)
        {
            item.GetComponent<Item>().Constructor("slime", 1, 0, "M");
        }
        else if(chance < slimeChance + potionChance)
        {
            item.GetComponent<Item>().Constructor("health potion", 1, 0, "HP");
        }
        else
        {
            item = null;
        }
    }
}
