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

    //attacking
    public GameObject attackHitbox;
    public int damage;
    public int damageScale;

    private Animator _animator;

    private IEnumerator coroutine;

    HealthSystem healthSystem = new HealthSystem(100);
    LevelingSystem levelingSystem = new LevelingSystem();

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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
            transform.localScale = new Vector3(1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction.y = direction.y + 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            direction.x = direction.x - 1;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction.y = direction.y - 1;
        }

        // -------------------------------------------

        if (Input.GetKeyUp(KeyCode.D))
        {
            direction.x = direction.x - 1;
            if(direction.x != 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        if(Input.GetKeyUp(KeyCode.A))
        {
            direction.x = direction.x + 1;
            if (direction.x != 0)
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
            _animator.SetBool("Run", false);
        }
        else if(!direction.Equals(Vector2.zero) && _animator.GetBool("Idle")) //start run animation if player is moving and is idle
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Run", true);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && canMove)
        {
            _animator.SetTrigger("Attack");
            _animator.SetBool("Idle", true);
            canMove = false;
            Invoke("Attack", 0.17f);
            Invoke("Attack", 0.25f); //collision with sword is active for 8 seconds
            Invoke("CanMoveAgain",1); //player will be able to walk after 1 second
        }

        if(canMove) //if player is able to walk
        {
            Vector2 movement = direction.normalized * speed * Time.deltaTime; 
            transform.Translate(movement);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !protection)
        {
            protection = true;
            healthSystem.Damage(collision.GetComponent<Slime>().damage);
            ((Slider)GameObject.FindObjectsOfType(typeof(Slider))[0]).GetComponent<PlayerBars>().UpdateHealthBar(healthSystem.GetHealth(), healthSystem.GetMaxHealth());

            coroutine = RemoveProtection(0.2f);
            StartCoroutine(coroutine);

            //collision.GetComponent<Slime>().Push();
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
        ((Slider)GameObject.FindObjectsOfType(typeof(Slider))[0]).GetComponent<PlayerBars>().UpdateExpBar(levelingSystem.currentExp, levelingSystem.maxExp[levelingSystem.level]); //updating EXP bar
    }

    private void BaseStatsUp() //Raising stats after reaching next level
    {
        damage = damage + damageScale;
        healthSystem.setHealth(healthScale);
        ((Slider)GameObject.FindObjectsOfType(typeof(Slider))[0]).GetComponent<PlayerBars>().UpdateHealthBar(healthSystem.GetHealth(), healthSystem.GetMaxHealth()); //updating health bar
    }

    IEnumerator RemoveProtection(float time) //Function in loop that happens after player is hit by an enemy
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
}
