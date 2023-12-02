using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlace : MonoBehaviour
{
    private bool triggered = false;
    [SerializeField] GameObject boss;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!triggered)
            {
                boss.GetComponent<SlimeBoss>().duringFight = true;
                boss.GetComponent<SlimeBoss>().allowMovement();
                triggered = true;
            }
        }
    }
}
