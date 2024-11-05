using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public Animator anim;
    public String currentAnimName;
    public int coint;
    public bool isDead = false;
    
    //private SpriteRenderer sprite;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
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
        }

        if (other.CompareTag("Coin"))
        {
            coint++;
            Destroy(other.gameObject);
            SoundManager.Instance.PlayVFXSound(1);
        }

        if (other.CompareTag("Spike"))
        {
            if (!isDead)
            {
                ChangeAnim("Die");  
                rb.velocity = Vector2.zero;
                StartCoroutine(ReLoad());
            }
          
        }

        if (other.CompareTag("Gate"))
        {
            if (other.GetComponent<GameManager>().hasWon)
            {
                rb.velocity = Vector2.zero;
                transform.position = other.transform.position;
            }
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

    IEnumerator ReLoad()
    {
        yield return new WaitForSeconds(1);
        ReloadCurrentScene();
    }
    public void ReloadCurrentScene()
    {
        // Lấy tên của scene hiện tại 
        string currentSceneName = SceneManager.GetActiveScene().name;
        //Tải lại scene hiện tại
        SceneManager.LoadScene(currentSceneName);
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
