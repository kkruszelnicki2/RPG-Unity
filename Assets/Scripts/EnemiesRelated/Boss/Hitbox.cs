using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private bool isColliding = false;

    public bool IsColliding()
    {
        return isColliding;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isColliding)
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && isColliding)
        {
            isColliding = false;
        }
    }
}
