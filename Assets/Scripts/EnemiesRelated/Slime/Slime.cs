using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private GameObject Player;
    public GameObject Place;
    private GameObject Spawner;

    private Animator _animator;

    public int id;

    //Movement
    public float speed;
    private float tempSpeed;
    public float sightRange;
    private int hasDestination = 0;

    //Damage related
    private bool isDamaged = false;
    public int hitTicks;
    public int damage;

    //Vectors
    private Vector2 direction;
    private Vector2 hitDirection = Vector2.zero;
    private Vector2 destinationPoint;

    //Loot
    public int exp = 2;

    public HealthSystem healthSystem = new HealthSystem(100);

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player"); //przypisanie obiektu gracza
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(Player.transform.position,transform.position) < sightRange && !isDamaged) //je¿eli gracz znajduje siê w polu widzenia przeciwnika
        {
            //sprawdzanie po³o¿enia przeciwnika wzglêdem gracza
            direction.x = (Player.transform.position.x - transform.position.x);
            direction.y = (Player.transform.position.y - transform.position.y);
            //ruch przeciwnika
            float scaleX;
            if (transform.position.x - Player.transform.position.x == 0)
            {
                scaleX = 1;
            }
            else
            {
                scaleX = -1 * Mathf.Abs(transform.localScale.x) * direction.x / (Mathf.Abs(transform.position.x - Player.transform.position.x));
            }
            transform.localScale = new Vector3(scaleX, transform.localScale.y, 1); //obracanie spritem przeciwnika gdy idzie w lewo/prawo
            _animator.SetBool("Idle",false);
            _animator.SetBool("Run", true);
            transform.Translate(direction.normalized * speed * Time.deltaTime); //ruch przeciwnika (normalizacja wektora, uwzglêdnienie czasu oraz prêdkoœci)
        }
        else if(isDamaged)
        {
            transform.Translate(hitDirection.normalized * tempSpeed * Time.deltaTime);
            _animator.SetBool("Idle", false);
            _animator.SetBool("Run", false);
        }   
        else if(Vector2.Distance(Player.transform.position, transform.position) >= sightRange)
        {
            if(hasDestination == 1)
            {
                transform.Translate(destinationPoint.normalized * speed * Time.deltaTime);
                _animator.SetBool("Idle", false);
                _animator.SetBool("Run", true);
            }
            else if(hasDestination == 0)
            {
                destinationPoint = new Vector2(Random.Range(Place.transform.position.x - 0.5f, Place.transform.position.x + 0.5f * Place.transform.localScale.x) - transform.position.x, 
                                                Random.Range(Place.transform.position.y - 0.5f, Place.transform.position.y + 0.5f * Place.transform.localScale.y) - transform.position.y);
                transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x) * destinationPoint.x / Mathf.Abs(destinationPoint.x), transform.localScale.y, 1); //obracanie spritem przeciwnika gdy idzie w lewo/prawo
                hasDestination = 1;
                Invoke("RemoveDestination", 2.62f);
            }
        }
    }

    //Kolizje
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "attackHitbox" && !isDamaged) //Kolizja z broni¹ gracza
        {
            healthSystem.Damage(Player.GetComponent<PlayerController>().damage);

            isDamaged = true;
            tempSpeed = speed;
            hitDirection.x = transform.position.x - Player.transform.position.x;
            hitDirection.y = transform.position.y - Player.transform.position.y;

            Push();

            if (healthSystem.GetHealth() <= 0)
            {
                gameObject.GetComponent<SlimeDeath>().Death(Spawner,Player);
            }
            gameObject.GetComponentInChildren<HealthBarUpdate>().HealthUpdate(healthSystem.GetMaxHealth(), healthSystem.GetHealth());
        }
    }
    //Odepchniêcie potwora gdy otrzyma atak
    public void Push()
    {
        if(!isDamaged)
        {
            isDamaged = true;
            tempSpeed = speed;
            hitDirection.x = transform.position.x - Player.transform.position.x;
            hitDirection.y = transform.position.y - Player.transform.position.y;
        }
        if(tempSpeed > 0)
        {
            tempSpeed = tempSpeed - speed / hitTicks;
            Invoke("Push",0.1f);
        }
        else
        {
            isDamaged = false;
        }
    }

    private void RemoveDestination()
    {
        hasDestination = 2;
        Invoke("CreateDestination", 1);
        _animator.SetBool("Idle", true);
        _animator.SetBool("Run", false);
    }
    
    private void CreateDestination()
    {
        hasDestination = 0;
    }
    public void Constructor(GameObject space,GameObject spawner,int newID)
    {
        this.Place = space;
        this.Spawner = spawner;
        this.id = newID;
    }

    public GameObject GetSpawner()
    {
        return Spawner;
    }
}
