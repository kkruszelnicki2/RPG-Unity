using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HealthStatModifier : CharacterStatModifierSO
{
    public override void AffectCharacter(GameObject character, int statValue)
    {
        character.GetComponent<PlayerController>().healthSystem.Heal(statValue);
        character.GetComponent<PlayerBars>().UpdateHealthBar(character.GetComponent<PlayerController>().healthSystem.GetHealth(),
            character.GetComponent<PlayerController>().healthSystem.GetMaxHealth());
    }
}
