using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    private Slider hBar;

    private void Awake()
    {
        hBar = (Slider)GameObject.FindObjectsOfType(typeof(Slider))[1];
    }
    public void Show()
    {
        hBar.transform.position = new Vector2(543f, 500f);
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
