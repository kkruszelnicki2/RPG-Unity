using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected GameObject Spawner;
    protected GameObject Place;
    protected Animator _animator;

    public int id;

    //loot
    public int exp;

    //stats
    public float speed;
    public float sightRange;
    public int damage;

    abstract public void Death();
    public GameObject GetSpawner()
    {
        return Spawner;
    }

    abstract public void GetHit();
}
