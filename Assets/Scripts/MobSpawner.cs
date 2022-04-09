using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject monster;

    private int mobCounter = 0;
    public int maxMobs = 3;
    private bool isActive = true;

    public float spawnCooldown = 3;
    public float activeCooldown = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mobCounter < maxMobs && isActive)
        {
            mobCounter++;
            isActive = false;
            Invoke("ActiveSpawner", activeCooldown);
            Invoke("spawn", spawnCooldown);
        }
    }

    private void spawn()
    {
        GameObject slime = (GameObject)GameObject.Instantiate(monster,new Vector2( transform.position.x, transform.position.y), Quaternion.identity);
        slime.GetComponent<Slime>().Constructor(transform.parent.transform.gameObject,gameObject);
    }

    public void ReduceCounter()
    {
        mobCounter--;
    }

    private void ActiveSpawner()
    {
        isActive = true;
    }
}
