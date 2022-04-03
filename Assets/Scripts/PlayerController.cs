using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Moving
    public float speed;
    private Vector2 direction;
    private bool canMove = true;

    //attacking
    public GameObject attackHitbox;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        direction.x = 0;
        direction.y = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
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

        if(direction.Equals(Vector2.zero) && !_animator.GetBool("Idle"))
        {
            _animator.SetBool("Idle", true);
            _animator.SetBool("Run", false);
        }
        else if(!direction.Equals(Vector2.zero) && _animator.GetBool("Idle"))
        {
            _animator.SetBool("Idle", false);
            _animator.SetBool("Run", true);
        }

        if(Input.GetKeyDown(KeyCode.Mouse0) && canMove)
        {
            _animator.SetTrigger("Attack");
            _animator.SetBool("Idle", true);
            canMove = false;
            Invoke("attack", 0.17f);
            Invoke("attack", 0.25f);
            Invoke("canMoveAgain",1);
        }

        if(canMove)
        {
            Vector2 movement = direction.normalized * speed * Time.deltaTime;
            transform.Translate(movement);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            //Debug.Log("Trigger");
        }
    }

    void canMoveAgain()
    {
        canMove = true;
    }

    void attack()
    {
        attackHitbox.SetActive(!attackHitbox.activeSelf);
    }
}
