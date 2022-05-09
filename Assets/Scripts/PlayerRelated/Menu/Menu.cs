using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    bool isVisible = false;

    Vector2 outOfGame = new Vector2(1300, 450);
    Vector2 inGame = new Vector2(500, 450);
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Move();
        }
    }

    public void Move()
    {
        if (isVisible)
        {
            transform.position = outOfGame;
        }
        else
        {
            transform.position = inGame;
        }
        isVisible = !isVisible;
    }
}
