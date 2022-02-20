using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool flip;

    private SpriteRenderer spRender;
    // Start is called before the first frame update
    void Start()
    {
        spRender = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flip)
        {
            transform.Translate(Vector2.right * 3 * Time.deltaTime);
            spRender.flipX = true;
        }
        else
        {
            transform.Translate(Vector2.left * 3 * Time.deltaTime);
            spRender.flipX = false;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Wall"))
        {
            flip = !flip;
        }
    }
}
