using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool isCollision = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsColliding()) //Start Dialogue after clicking E
        {
            Interaction();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCollision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCollision = false;
        }
    }

    protected bool IsColliding()
    {
        return isCollision;
    }

    protected abstract void Interaction();
}
