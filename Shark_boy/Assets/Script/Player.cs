using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public Animator anim;
    public String currentAnimName;
    //private SpriteRenderer sprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity == Vector2.zero)
        {
            ChangeAnim("Idle");
        }
        else
        {
            
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                ChangeAnim("Move");
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                ChangeAnim("Move");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Port"))
        {
            rb.velocity = Vector2.zero;
            transform.position = other.transform.position;
          
        
        }
        if (other.gameObject.CompareTag("Ground"))
        {
            // Đảo ngược chiều di chuyển của Player
            rb.velocity = -1*rb.velocity;
            Debug.Log(rb.velocity);
            Debug.Log("revert");
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Port"))
        {
            if (Vector3.Distance(transform.position, other.transform.position) < 0.01f)
            {
                rb.velocity = Vector2.zero;
            }
        
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Đảo ngược chiều di chuyển của Player
            rb.velocity = -1*rb.velocity;
            Debug.Log(rb.velocity);
            Debug.Log("revert");
        }
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(animName);
        }

    }
}
