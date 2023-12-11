using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDrop : MonoBehaviour
{
    [SerializeField] List<Item> loots;
    [SerializeField] private GameObject itemDropped;
    public Item Drop()
    {
        foreach(Item loot in loots)
        {
            int chance = Random.Range(1, 101);
            if (chance <= loot.chance)
            {
                return loot;
            }
        }

        return null;

    }
}
