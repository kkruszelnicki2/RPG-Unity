using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [Header("Only gameplay")]
    public int price;
    public int chance;

    [Header("Only UI")]
    public bool stackable;

    [Header("Both")]
    public Sprite sprite;
    public string itemName;
}

public interface IDestroyableItem
{

}

public interface IItemAction
{
    public string ActionName { get; }
    //public AudioClip actionSFX { get; }
    bool PerformAction(GameObject character);
}

[Serializable]
public class ModifierData
{
    public CharacterStatModifierSO statModifier;
    public int value;
}
