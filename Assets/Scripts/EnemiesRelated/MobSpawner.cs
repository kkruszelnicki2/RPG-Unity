using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public GameObject monster;

    public static int idNext = 0;
    public int id;

    private int mobCounter = 0;
    public int maxMobs = 3;
    private bool isActive = true;

    public float spawnCooldown = 3;

    private void Awake()
    {
        this.id = idNext++;
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public void ReduceCounter()
    {
        mobCounter--;
    }

    private IEnumerator Spawn()
    {
        while(isActive)
        {
            yield return new WaitForSeconds(spawnCooldown);

            if (mobCounter < maxMobs)
            {
                mobCounter++;
                GameObject slime = GameObject.Instantiate(monster, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
                slime.GetComponent<Slime>().Constructor(transform.parent.transform.gameObject, gameObject, id);
            }
        }
    }

    public void ForceSpawn(int health,Vector3 pos)
    {
        mobCounter++;
        GameObject slime = GameObject.Instantiate(monster, pos, Quaternion.identity);
        slime.GetComponent<Slime>().Constructor(transform.parent.transform.gameObject, gameObject,id);
        slime.transform.position = pos;
        slime.GetComponent<Slime>().healthSystem.SetHealth(health);
        slime.GetComponentInChildren<HealthBarUpdate>().HealthUpdate(slime.GetComponent<Slime>().healthSystem.GetMaxHealth(), slime.GetComponent<Slime>().healthSystem.GetHealth());
    }
}