using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MaterialItemSO : Item, IDestroyableItem, IItemAction
{
    public string ActionName => "Nothing";

    public bool PerformAction(GameObject character)
    {
        return true;
    }
}
