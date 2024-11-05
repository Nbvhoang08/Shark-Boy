using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Player player;
    [SerializeField] private int Coins;
    public bool hasWon => Coins == player.coint;
    private Animator anim;
    private void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        anim.SetBool("Locked", !hasWon);
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && hasWon)
        {
            StartCoroutine(NextLevel());
            LevelManager.Instance.SaveGame();
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1f);
        UIManager.Instance.OpenUI<WinCanvas>();

    }
}
