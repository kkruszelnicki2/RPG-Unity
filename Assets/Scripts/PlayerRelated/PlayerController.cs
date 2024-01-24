using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Health
    public int baseHealth;
    public int healthScale;
    private bool protection = false;

    //Movement
    public float speed;
    private Vector2 direction;
    private bool canMove = true;
    private Vector3 movement;
    private Rigidbody2D playerRigidbody2D;

    //attacking
    public GameObject attackHitbox;
    public int damage;
    public int baseDamage;
    public int damageScale;

    private Animator _animator;

    private IEnumerator coroutine;

    public HealthSystem healthSystem = new HealthSystem(100);
    public LevelingSystem levelingSystem = new LevelingSystem();

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start() // Start is called before the first frame update
    {
        direction.x = 0;
        direction.y = 0;
    }

    void Update() // Update is called once per frame
    {
        if (Input.GetKeyDown(KeyCode.D)) //moving with WSAD
        {
            direction.x = direction.x + 1;
            if (!GameObject.Find("Game").GetComponent<Game>().dialogue)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction.y = direction.y + 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            direction.x = direction.x - 1;
            if (!GameObject.Find("Game").GetComponent<Game>().dialogue)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction.y = direction.y - 1;
        }

        // -------------------------------------------

        if (Input.GetKeyUp(KeyCode.D))
        {
            direction.x = direction.x - 1;
            if(direction.x != 0 && !GameObject.Find("Game").GetComponent<Game>().dialogue)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            direction.x = direction.x + 1;
            if (direction.x != 0 && !GameObject.Find("Game").GetComponent<Game>().dialogue)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            direction.y = direction.y + 1;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            direction.y = direction.y - 1;
        }

        if(direction.Equals(Vector2.zero) && !_animator.GetBool("Idle")) //start idle animation if player isn't moving and isn't idle
        {
            _animator.SetBool("Idle", true);
        }
        else if(!direction.Equals(Vector2.zero) && _animator.GetBool("Idle") && !GameObject.Find("Game").GetComponent<Game>().dialogue) //start run animation if player is moving and is idle
        {
            _animator.SetBool("Idle", false);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && canMove && !GameObject.Find("Game").GetComponent<Game>().dialogue)
        {
            _animator.SetTrigger("Attack");
            _animator.SetBool("Idle", true);
            canMove = false;
            Invoke("Attack", 0.17f);
            Invoke("Attack", 0.25f); //collision with sword is active for 8 seconds
            Invoke("CanMoveAgain",1.1f); //player will be able to walk after 1 second
        } 
    }

    public void DontMove()
    {
        _animator.SetBool("Idle", true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !protection)
        {
            GetAttacked(collision.GetComponent<Slime>().damage);

        }
    }

    private void FixedUpdate()
    {
        if (canMove && !GameObject.Find("Game").GetComponent<Game>().dialogue) //if player is able to walk
        {
            movement = direction.normalized * speed * Time.deltaTime;
            playerRigidbody2D.MovePosition(transform.position + movement);
        }
    }

    private void CanMoveAgain()
    {
        canMove = true;
    }

    private void Attack()
    {
        attackHitbox.SetActive(!attackHitbox.activeSelf);
    }

    public void Experience(int Exp)
    {
        if(levelingSystem.GrantExp(Exp)) //if enough EXP, level up
        {
            BaseStatsUp();
        }
        GetComponent<PlayerBars>().UpdateExpBar(levelingSystem.currentExp, levelingSystem.maxExp[levelingSystem.level]); //updating EXP bar
    }

    private void BaseStatsUp() //Raising stats after reaching next level
    {
        damage = damage + damageScale;
        healthSystem.UpgradeHealth(healthScale);
        GetComponent<PlayerBars>().UpdateHealthBar(healthSystem.GetHealth(), healthSystem.GetMaxHealth()); //updating health bar
    }

    IEnumerator RemoveProtection(float time) //Function in loop that happens after player is hit by the enemy
    {
        while(time >= 0.001f) //Player can't get hit while his protection is active
        {
            GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled; //Making player invisible and visible
            time = time - 0.01f;
            yield return new WaitForSeconds(time); //wait for x seconds
        }
        
        protection = false;
        yield break; //ending loop
    }

    //After clicking "New Game"
    public void PlayerReset()
    {
        transform.position = new Vector3(0.167f,-0.076f,0);

        damage = baseDamage;
        healthSystem.resetHealth(baseHealth);
        GetComponent<PlayerBars>().UpdateHealthBar(healthSystem.GetHealth(), healthSystem.GetMaxHealth());

        levelingSystem.ResetLevel();
        GetComponent<PlayerBars>().UpdateExpBar(levelingSystem.currentExp, levelingSystem.maxExp[levelingSystem.level]); //updating EXP bar

        GameObject.Find("InventoryUI").GetComponent<Inventory>().ResetEquipment();
    }

    public void GetAttacked(int damage)
    {
        protection = true;
        healthSystem.Damage(damage);
        GetComponent<PlayerBars>().UpdateHealthBar(healthSystem.GetHealth(), healthSystem.GetMaxHealth());

        coroutine = RemoveProtection(0.2f);
        StartCoroutine(coroutine);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemClass item = collision.GetComponent<ItemClass>();
        if(item != null)
        {
            GameObject.Find("InventoryUI").GetComponent<Inventory>().AddItem(item);
            item.DestroyItem();
        }
    }
}
