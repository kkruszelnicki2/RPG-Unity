using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDeath : MonoBehaviour
{
    public GameObject item;
    public void Death(GameObject spawner, GameObject Player)
    {

        Player.GetComponent<PlayerController>().Experience(gameObject.GetComponent<Slime>().exp);

        spawner.GetComponent<MobSpawner>().ReduceCounter();

        GameObject itemInstance = Instantiate(item, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        GetComponent<SlimeDrop>().Drop(itemInstance);

        if(itemInstance.GetComponent<Item>().itClass.GetName() == null)
        {
            Destroy(itemInstance.gameObject);
        }

        Destroy(gameObject);
    }
}

