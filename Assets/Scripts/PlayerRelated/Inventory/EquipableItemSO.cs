using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EquipableItemSO : Item, IDestroyableItem, IItemAction
{
    public string ActionName => "Equiped";

    public bool PerformAction(GameObject character)
    {
        throw new System.NotImplementedException();
    }
}
