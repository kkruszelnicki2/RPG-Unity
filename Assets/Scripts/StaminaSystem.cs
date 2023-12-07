using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaSystem
{
    public int currentStamina;
    public int maxStamina;
    private float staminaRegenTimer;
    public float staminaRegenRate; // czas potrzebny do regeneracji 1 punktu staminy

    // Konstruktor
    public StaminaSystem(int initialStamina)
    {
        maxStamina = initialStamina;
        currentStamina = maxStamina;
        staminaRegenRate = 1.0f; // Domyślna wartość, może być dostosowana
    }

    // Użycie staminy
    public bool UseStamina(int amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            return true; // Użycie staminy powiodło się
        }
        else
        {
            return false; // Niewystarczająca ilość staminy
        }
    }

    // Regeneracja staminy
    public void RegenerateStamina()
    {
        if (currentStamina < maxStamina)
        {
            staminaRegenTimer += Time.deltaTime;
            if (staminaRegenTimer >= staminaRegenRate)
            {
                currentStamina++;
                staminaRegenTimer = 0f;
            }
        }
    }

    // Zresetowanie staminy do maksymalnej wartości
    public void ResetStamina()
    {
        currentStamina = maxStamina;
    }

    // Ustawienie nowej maksymalnej staminy (np. po level up)
    public void SetMaxStamina(int newMaxStamina)
    {
        maxStamina = newMaxStamina;
        currentStamina = maxStamina;
    }
}