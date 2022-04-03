using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private GameObject Player;
    public PlayerController Playercontroller;

    public float speed;
    private float temp_speed;
    public float sight_range;

    private bool is_damaged = false;
    public int hit_ticks;

    private Vector2 direction;
    private Vector2 hit_direction = Vector2.zero;

    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player"); //przypisanie obiektu gracza
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(Player.transform.position,transform.position) < sight_range && !is_damaged) //je¿eli gracz znajduje siê w polu widzenia przeciwnika
        {
            //sprawdzanie po³o¿enia przeciwnika wzglêdem gracza
            direction.x = (Player.transform.position.x - transform.position.x) / Mathf.Abs(transform.position.x - Player.transform.position.x);
            direction.y = (Player.transform.position.y - transform.position.y) / Mathf.Abs(transform.position.y - Player.transform.position.y);
            //ruch przeciwnika
            transform.localScale = new Vector3(-0.54f * direction.x, 0.54f, 1); //obracanie spritem przeciwnika gdy idzie w lewo/prawo
            transform.Translate(direction.normalized * speed * Time.deltaTime); //ruch przeciwnika (normalizacja wektora, uwzglêdnienie czasu oraz prêdkoœci)
        }
        else if(is_damaged)
        {
            transform.Translate(hit_direction.normalized * temp_speed * Time.deltaTime);
        }   
    }

    //Kolizje
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "attackHitbox") //Kolizja z broni¹ gracza
        {
            is_damaged = true;
            temp_speed = speed;
            hit_direction.x = transform.position.x - Player.transform.position.x;
            hit_direction.y = transform.position.y - Player.transform.position.y;

            Push();
        }
    }

    private void Push()
    {
        if(temp_speed > 0)
        {
            temp_speed = temp_speed - speed / hit_ticks;
            Invoke("Push",0.1f);
        }
        else
        {
            is_damaged = false;
        }
    }
}
