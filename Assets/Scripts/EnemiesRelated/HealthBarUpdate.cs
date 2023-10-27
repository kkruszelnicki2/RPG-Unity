using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarUpdate : MonoBehaviour
{
    private float basicScale;
    private float newScale;
    public void Awake()
    {
        basicScale = transform.localScale.x;
    }
    public void HealthUpdate(int maxHealth,int health)
    {
        if(health <= 0)
        {
            newScale = 0;
        }
        else
        {
            newScale = basicScale * ((float)health / (float)maxHealth);
        }
        transform.localScale = new Vector3(newScale,transform.localScale.y,transform.localScale.z);
    }
}
