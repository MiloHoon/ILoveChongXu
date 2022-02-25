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
    private bool goldDoorOpened, silverDoorOpened, bronzeDoorOpened;

    private Vector2 goldDoorPos, silverDoorPos, bronzeDoorPos;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    [Space(10)]
    public List<GameObject> LIST_DOOR;

    [SerializeField]
    [Space(10)]
    public AudioSource audioSource;
    public AudioClip winClip, loseClip, jumpClip, coinClip, walkClip, doorClip, hurtClip;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        goldDoorPos.y = LIST_DOOR[0].transform.position.y - 1.5f;
        silverDoorPos.x = LIST_DOOR[1].transform.position.x - 1.5f;
        bronzeDoorPos.y = LIST_DOOR[2].transform.position.y - 1.5f;
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
        CheckDoorsOpen();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = true;
            animator.SetBool("isGrounded", true);
        }
        else
            onGround = false;
        
        //Enemy Collision
        if(collision.gameObject.tag == "Enemy")
        {
            audioSource.PlayOneShot(hurtClip);
            GameManager.instance.MinusHealth();
        }

        //Collide With Door
        if (collision.gameObject.tag == "GoldDoor")
        {
            if (GameManager.instance.goldCollected == GameManager.instance.goldCount)
            {
                audioSource.PlayOneShot(doorClip);
                goldDoorOpened = true;
            }
        }
        else if (collision.gameObject.tag == "SilverDoor")
        {
            if (GameManager.instance.silverCollected == GameManager.instance.silverCount)
            {
                audioSource.PlayOneShot(doorClip);
                silverDoorOpened = true;
            }
        }
        else if (collision.gameObject.tag == "BronzeDoor")
        {
            if (GameManager.instance.bronzeCollected == GameManager.instance.bronzeCount)
            {
                audioSource.PlayOneShot(doorClip);
                bronzeDoorOpened = true;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Spike Collision
        if (collision.gameObject.tag == "Spike")
        {
            audioSource.PlayOneShot(hurtClip); 
            GameManager.instance.MinusHealth();
        }

        //Collide With Flag
        if (collision.gameObject.tag == "EndGoal")
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
            audioSource.clip = winClip;
            audioSource.Play();
            audioSource.loop = false ;
            GameManager.instance.WinLoseScene(true);
        }

        //Collide With Coins
        if (collision.gameObject.CompareTag("Gold"))
        {
            audioSource.PlayOneShot(coinClip);
            GameManager.instance.goldCollected++;
            Destroy(collision.gameObject);
            
        }
        else if (collision.gameObject.CompareTag("Silver"))
        {
            audioSource.PlayOneShot(coinClip);
            GameManager.instance.silverCollected++;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Bronze"))
        {
            audioSource.PlayOneShot(coinClip);
            GameManager.instance.bronzeCollected++;
            Destroy(collision.gameObject);
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
            if ( !audioSource.isPlaying && onGround == true)
                audioSource.PlayOneShot(walkClip);

            transform.position += transform.right * moveSpeed * Time.deltaTime;
            //Flip Sprite To Right Side
            spriteRenderer.flipX = false;
            //Play Running Animation
            animator.SetBool("isRunning", true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (!audioSource.isPlaying && onGround == true)
                audioSource.PlayOneShot(walkClip);

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
            audioSource.clip = jumpClip;
            audioSource.Play();

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

    private void CheckDoorsOpen()
    {
        if (goldDoorOpened == true)
        {
            if (LIST_DOOR[0].transform.position.y >= goldDoorPos.y)
            {
                LIST_DOOR[0].transform.Translate(Vector3.down * 3 * Time.deltaTime);
                BoxCollider2D bx = LIST_DOOR[0].GetComponent<BoxCollider2D>();
                bx.isTrigger = true;
            }
        }
        
        if (silverDoorOpened == true)
        {
            if (LIST_DOOR[1].transform.position.x >= silverDoorPos.x)
            {
                LIST_DOOR[1].transform.Translate(Vector3.down * 3 * Time.deltaTime);
                BoxCollider2D bx = LIST_DOOR[1].GetComponent<BoxCollider2D>();
                bx.isTrigger = true;
            }
        }
        
        if (bronzeDoorOpened == true)
        {
            if (LIST_DOOR[2].transform.position.y >= bronzeDoorPos.y)
            {
                LIST_DOOR[2].transform.Translate(Vector3.down * 3 * Time.deltaTime);
                BoxCollider2D bx = LIST_DOOR[2].GetComponent<BoxCollider2D>();
                bx.isTrigger = true;
            }
        }
    }
}