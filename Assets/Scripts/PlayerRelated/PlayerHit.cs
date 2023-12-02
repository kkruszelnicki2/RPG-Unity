using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetHit();
        }
        else if(other.CompareTag("Boss"))
        {
            other.GetComponent<Boss>().GetHit();
        }
    }
}
