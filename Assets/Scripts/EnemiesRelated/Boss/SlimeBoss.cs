using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : Boss
{
    public bool duringFight = false;
    private bool isDamaged = false;

    //Movement
    private bool canMove = false;
    private bool isMovementTimerSet = false;
    private Vector2 direction;
    [SerializeField] float speed;
    private int movementTime = 2;
    private int movementCooldown = 1;
    private float scaleX;

    float tempDirection;

    //Attacks
    private bool duringAttack = false;

    //Attack
    [SerializeField] GameObject attackHitbox;
    bool canAttack = false;
    [SerializeField] float attackCooldown = 5.0f;
    [SerializeField] float attackCastTime = 3.0f;
    //Jump
    [SerializeField] GameObject jumpHitbox;
    bool canJump = false;
    bool duringJump = false;
    [SerializeField] float jumpCooldown = 5.0f;
    [SerializeField] float jumpCastTime = 3.0f;

    //Statistics
    private int attackDamage = 30;
    private int jumpDamage = 50;
    public HealthSystem healthSystem = new HealthSystem(100);
    private int exp = 100;

    // Update is called once per frame
    void Update()
    {
        if (duringFight)
        {
            if(firstTime)
            {
                Invoke("AllowSkills", 1);
                firstTime = false;
                bossBar.Show();
            }
            if (canAttack && Vector2.Distance(gameObject.transform.position, player.transform.position) < 0.8 && !duringAttack) //attack
            {
                canAttack = false;
                duringAttack = true;
                _animator.SetBool("Idle", true);
                _animator.SetBool("Moving", false);
                Color color = attackHitbox.GetComponent<SpriteRenderer>().color;
                color.a = 0.5f;
                attackHitbox.GetComponent<SpriteRenderer>().color = color;
                Invoke("Attack", attackCastTime);
            }
            else if(canJump && !duringAttack) //jump
            {
                canJump = false;
                duringAttack = true;
                _animator.SetTrigger("Jump");
                Color color = jumpHitbox.GetComponent<SpriteRenderer>().color;
                color.a = 0.5f;
                jumpHitbox.GetComponent<SpriteRenderer>().color = color;
                Invoke("Jump", jumpCastTime);
            }
            else if (canMove && Vector2.Distance(gameObject.transform.position, player.transform.position) > 0.6 && !duringAttack)
            {
                _animator.SetBool("Idle", false);
                if (!isMovementTimerSet)
                {
                    isMovementTimerSet = true;
                    Invoke("denyMovement", movementTime);
                }
                //sprawdzanie po³o¿enia przeciwnika wzglêdem gracza
                direction.x = (player.transform.position.x - transform.position.x);
                direction.y = (player.transform.position.y - transform.position.y);
                //ruch przeciwnika
                transform.Translate(direction.normalized * speed * Time.deltaTime); //ruch przeciwnika (normalizacja wektora, uwzglêdnienie czasu oraz prêdkoœci)
            }
            else if(canMove && !_animator.GetBool("Idle"))
            {
                _animator.SetBool("Idle", true);
            }

            if (transform.position.x - player.transform.position.x < 0)
            {
                scaleX = Mathf.Abs(transform.localScale.x);
            }
            else
            {
                scaleX = -1 * Mathf.Abs(transform.localScale.x);
            }
            transform.localScale = new Vector3(scaleX, transform.localScale.y, 1); //obracanie spritem przeciwnika gdy idzie w lewo/prawo
            if(duringJump)
            {
                transform.Translate(direction.normalized * tempDirection * Time.deltaTime);
                if(Vector2.Distance(gameObject.transform.position, jumpHitbox.transform.position) <= 0.1)
                {
                    Landing();
                    duringJump = false;
                }
            }
            else
            {
                jumpHitbox.transform.position = player.transform.position;
            }
        }
    }

    private void DuringAttack()
    {
        duringAttack = false;
    }

    private void AllowSkills()
    {
        canJump = true;
        canAttack = true;
    }

    public void allowMovement()
    {
        canMove = true;
        _animator.SetBool("Idle", false);
        _animator.SetBool("Moving", true);
    }

    public void denyMovement()
    {
        canMove = false;
        Invoke("allowMovement", movementCooldown);
        isMovementTimerSet = false;
        _animator.SetBool("Idle", true);
        _animator.SetBool("Moving", false);
    }

    private void CanAttack()
    {
        canAttack = true;
    }

    private void Attack()
    {
        _animator.SetTrigger("Attack");
        Color color = attackHitbox.GetComponent<SpriteRenderer>().color;
        color.a = 0f;
        attackHitbox.GetComponent<SpriteRenderer>().color = color;
        Invoke("CanAttack", attackCooldown);
        Invoke("DuringAttack", 1);

        if (attackHitbox.GetComponent<Hitbox>().IsColliding())
        {
            player.GetComponent<PlayerController>().GetAttacked(attackDamage);
        }
    }

    private void CanJump()
    {
        canJump = true;
    }

    private void Jump()
    {
        _animator.SetTrigger("JumpAir");
        duringJump = true;
        direction.x = jumpHitbox.transform.position.x - transform.position.x;
        direction.y = jumpHitbox.transform.position.y - transform.position.y;
        tempDirection = Vector2.Distance(gameObject.transform.position, jumpHitbox.transform.position);
    }
    private void Landing()
    {
        Color color = jumpHitbox.GetComponent<SpriteRenderer>().color;
        color.a = 0f;
        jumpHitbox.GetComponent<SpriteRenderer>().color = color;

        Invoke("DuringAttack", 1);
        duringJump = false;
        _animator.SetTrigger("JumpEnd");
        Invoke("CanJump", jumpCooldown);

        if(jumpHitbox.GetComponent<Hitbox>().IsColliding())
        {
            player.GetComponent<PlayerController>().GetAttacked(jumpDamage);
        }
    }

    private void IsNotDamaged()
    {
        isDamaged = false;
    }

    private void DefaultColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }

    override protected void Death()
    {
        Color color = jumpHitbox.GetComponent<SpriteRenderer>().color;
        color.a = 0f;
        jumpHitbox.GetComponent<SpriteRenderer>().color = color;

        bossBar.Hide();

        player.GetComponent<PlayerController>().Experience(exp);

        GameObject.Find("QuestLog").GetComponent<QuestLog>().Progress("Król Slime");

        Destroy(gameObject);
    }

    override public void GetHit()
    {
        if(!isDamaged)
        {
            healthSystem.Damage(player.GetComponent<PlayerController>().damage);

            isDamaged = true;
            Invoke("IsNotDamaged", 1);

            GetComponent<SpriteRenderer>().color = new Color(197 / 255f, 14 / 255f, 1 / 255f, 1);
            Invoke("DefaultColor", 0.1f);

            bossBar.UpdateHealthBar(healthSystem.GetHealth(), healthSystem.GetMaxHealth());

            if (healthSystem.GetHealth() <= 0)
            {
                Death();
            }
        }
    }
}
