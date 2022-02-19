using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpHeight;
    public float fallingThreshold;

    private bool onGround;
    private bool falling;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Lose();
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            onGround = true;
            animator.SetBool("isGrounded", true);
        }
        
        //Enemy B Collision
        if(collision.gameObject.tag == "Enemy")
        {
            //GameManager.instance.MinusHealth();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Enemy A Collision
        if(collision.gameObject.tag == "Enemy")
        {
            //GameManager.instance.MinusHealth();
        }

        if (collision.gameObject.tag == "Health")
        {
            //GameManager.instance.AddHealth();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Coins")
        {
            //GameManager.instance.AddCoins();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "EndGoal")
        {
            //GameManager.instance.WinScene();
            PlayerDied();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D hitPos in collision.contacts)
        {
            //Check If The Wall Collided On The Sides
            if (hitPos.normal.x != 0)
            {
                //Boolean To Prevent Player From Being Able To Jump
                onGround = false; 
            }
            //Check If Its Collided On Top
            else if (hitPos.normal.y > 0)  
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }
        }
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * moveSpeed * Time.deltaTime;
            //Flip Sprite To Right Side
            spriteRenderer.flipX = false;
            //Play Running Animation
            animator.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * -moveSpeed * Time.deltaTime;
            //Flip Sprite To Left Side
            spriteRenderer.flipX = true;
            //Play Running Animation
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void Jump()
    {
        //Jump
        if (Input.GetKey(KeyCode.Space) && onGround && falling == false)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                onGround = false;
            //Play Jumping Animation
            animator.SetTrigger("isJumping");
            animator.SetBool("isGrounded", false);
        }

        //Check If Player Is Falling
        if (rb.velocity.y < fallingThreshold)
        {
            falling = true;
        }
        else
        {
            falling = false;
        }
    }

    public void PlayerDied()
    {
        Destroy(gameObject);
    }
    
    public void Lose()
    {
        if (GameManager.instance.currentHealth <= 0)
        {
        //    GameManager.instance.LoseScene();
            PlayerDied();
        }
    }
}