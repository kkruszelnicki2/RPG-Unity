using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EdibleItemSO : Item, IDestroyableItem, IItemAction
{
    [SerializeField]
    private List<ModifierData> modifiersData = new List<ModifierData>();
    public string ActionName => "Consumed";

    public bool PerformAction(GameObject character)
    {
        foreach(ModifierData data in modifiersData)
        {
            data.statModifier.AffectCharacter(character, data.value);
        }

        return true;
    }
}


