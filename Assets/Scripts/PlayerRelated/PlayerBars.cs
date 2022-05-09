using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    private GameObject Player;
    public Image fillImage;
    private Slider hpBar;
    private Slider expBar;

    // Start is called before the first frame update
    void Awake()
    {
        hpBar = (Slider)GameObject.FindObjectsOfType(typeof(Slider))[0];
        expBar = (Slider)GameObject.FindObjectsOfType(typeof(Slider))[1];
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
}
