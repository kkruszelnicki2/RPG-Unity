using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject monster;

    public int id;

    private int mobCounter = 0;
    public int maxMobs = 3;
    private bool isActive = true;

    public float spawnCooldown = 3;
    public float activeCooldown = 6;

    private void Awake()
    {
        this.id = GameObject.Find("Game").GetComponent<Game>().GetID();
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
        GameObject slime = GameObject.Instantiate(monster,new Vector2( transform.position.x, transform.position.y), Quaternion.identity);
        slime.GetComponent<Slime>().Constructor(transform.parent.transform.gameObject,gameObject,id);
    }

    public void ReduceCounter()
    {
        mobCounter--;
    }

    private void ActiveSpawner()
    {
        isActive = true;
    }

    public void ForceSpawn(int health,Vector3 pos)
    {
        mobCounter++;
        GameObject slime = GameObject.Instantiate(monster, pos, Quaternion.identity);
        slime.GetComponent<Slime>().Constructor(transform.parent.transform.gameObject, gameObject, id);
        slime.transform.position = pos;
        slime.GetComponent<Slime>().healthSystem.SetHealth(health);
        slime.GetComponentInChildren<HealthBarUpdate>().HealthUpdate(slime.GetComponent<Slime>().healthSystem.GetMaxHealth(), slime.GetComponent<Slime>().healthSystem.GetHealth());
    }
}