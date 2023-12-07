using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    public Image fillImage;
    private Slider hpBar;
    private Slider expBar;
    private Slider staminaBar;
    
    // Start is called before the first frame update
    void Awake()
    {
        hpBar = GameObject.Find("HpBar").GetComponent<Slider>();
        expBar = GameObject.Find("ExpBar").GetComponent<Slider>();
        staminaBar  = GameObject.Find("StaminaBar").GetComponent<Slider>();
    }

    public void UpdateHealthBar(int playerHealth, int playerMaxHealth)
    {
        hpBar.value = (float)playerHealth / (float)playerMaxHealth;

        if (hpBar.value <= hpBar.minValue)
        {
            fillImage.enabled = false;
        }
    }
    public void UpdateExpBar(int playerExp, int playerMaxExp)
    {
        expBar.value = (float)playerExp / (float)playerMaxExp;
    }
    
    public void UpdateStaminaBar(int staminaSystemCurrentStamina, int staminaSystemMaxStamina)
    {
        if (staminaBar != null)
        {
            staminaBar.value = (float)staminaSystemCurrentStamina / (float)staminaSystemMaxStamina;
        }
        else
        {
            Debug.LogError("Stamina bar slider is not initialized!");
        }
    }
}
