using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    private Slider hBar;
    public Image fillImage;

    private void Awake()
    {
        hBar = (Slider)GameObject.FindObjectsOfType(typeof(Slider))[0];
    }
    public void Show()
    {
        hBar.transform.position = new Vector2(543f, 550f);
    }
    public void Hide()
    {
        hBar.transform.position = new Vector2(543f, 900f);
    }

    public void UpdateHealthBar(int Health, int MaxHealth)
    {
        hBar.value = (float)Health / (float)MaxHealth;
    }
}
