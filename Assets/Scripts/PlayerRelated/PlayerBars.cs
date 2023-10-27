using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBars : MonoBehaviour
{
    public Image fillImage;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider expBar;

    // Start is called before the first frame update
    void Awake()
    {
        //hpBar = (Slider)GameObject.FindObjectsOfType(typeof(Slider))[0];
        //expBar = (Slider)GameObject.FindObjectsOfType(typeof(Slider))[2];
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
