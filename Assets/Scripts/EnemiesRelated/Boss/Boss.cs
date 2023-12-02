using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : MonoBehaviour
{
    protected Animator _animator;
    protected GameObject player;
    protected BossBar bossBar;

    protected bool firstTime = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player"); //przypisanie obiektu gracza

        bossBar = GetComponent<BossBar>();
    }

    //public

    abstract public void GetHit();
    abstract protected void Death();
}
