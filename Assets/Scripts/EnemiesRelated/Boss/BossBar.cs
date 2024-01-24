using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBar : MonoBehaviour
{
    private Slider hBar;

    private void Awake()
    {
        hBar = GameObject.Find("BossHBar").GetComponent<Slider>();
    }
    public void Show()
    {
        hBar.transform.position = new Vector2(600f, 500f);
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
