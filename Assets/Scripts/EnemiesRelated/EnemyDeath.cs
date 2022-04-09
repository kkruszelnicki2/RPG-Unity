using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDeath : MonoBehaviour
{
    public void Death(GameObject spawner, GameObject Player)
    {
        spawner.GetComponent<MobSpawner>().ReduceCounter();

        Player.GetComponent<PlayerController>().Experience(gameObject.GetComponent<Slime>().exp);

        Destroy(gameObject);
    }
}
