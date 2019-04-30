using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public int health;
    public int numberHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public static int meleeDamagePlayer;

    public static int bowDamagePlayer;

    public static int magicDamagePlayer;

    public GameObject deathCanvas;

    public GameObject deathEffect;

    public GameObject gotHitSound;

    public AudioSource mySource;

    private bool healthPlayed;

    private bool clothPlayer;
    private bool bluePlayer;
    private bool redPlayer;


    // Start is called before the first frame update
    void Start()
    {
        meleeDamagePlayer = 2;
        bowDamagePlayer = 3;
        magicDamagePlayer = 4;
        healthPlayed = false;
        bluePlayer = GameObject.Find("Player").GetComponent<playerShoot>().highArmorPurchased;
        clothPlayer = GameObject.Find("Player").GetComponent<playerShoot>().clothPlayer;
        redPlayer = GameObject.Find("Player").GetComponent<playerShoot>().lowArmorPurchased;

    }

    // Update is called once per frame
    void Update()
    {
        clothPlayer = GameObject.Find("Player").GetComponent<playerShoot>().clothPlayer;
        if (clothPlayer == true)
        {
            meleeDamagePlayer = 3;
            bowDamagePlayer = 4;
            magicDamagePlayer = 5;
        }

        bluePlayer = GameObject.Find("Player").GetComponent<playerShoot>().highArmorPurchased;
        if (bluePlayer == true)
        {
            meleeDamagePlayer = 1;
            bowDamagePlayer = 2;
            magicDamagePlayer = 3;
        }

        redPlayer = GameObject.Find("Player").GetComponent<playerShoot>().lowArmorPurchased;
        if (redPlayer == true)
        {
            meleeDamagePlayer = 2;
            bowDamagePlayer = 3;
            magicDamagePlayer = 4;
        }

        if (health > numberHearts)
        {
            health = numberHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < numberHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (health <= 4)
        {
            LowHealth();
            

        }

        if (health > 4)
        {
            healthPlayed = false;
        }

        if (health <= 0)
        {
            Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            YouLose();
        }
    }

    public void PlayerTakeMeleeDamage(int meleeDamagePlayer)
    {

        health -= meleeDamagePlayer;
        gotHitSound.GetComponent<soundRandomizer>().PlayActive() ;
        

    }

    public void PlayerTakeBowDamage(int bowDamagePlayer)
    {
        health -= bowDamagePlayer;
        gotHitSound.GetComponent<soundRandomizer>().PlayActive();

    }

    public void PlayerTakeMagicDamage(int magicDamagePlayer)
    {
        health -= magicDamagePlayer;
        gotHitSound.GetComponent<soundRandomizer>().PlayActive();
    }

    public void LowHealth()
    {
        if (healthPlayed == false)
        {
            mySource.Play();
            healthPlayed = true;
        }
        
    }

    public void YouLose()
    {
        
        GameObject theController = GameObject.Find("GameManager");
        menuController menuController = theController.GetComponent<menuController>();
        menuController.gameStart = false;
        Time.timeScale = 0f;
        deathCanvas.SetActive(true);
    }
}
