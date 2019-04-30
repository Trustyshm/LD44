using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    public float maxHealth;
    private float currentHealth;

    public GameObject deathEffect;

    public GameObject gotHitSound;

    public GameObject victoryScreen;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Dead");
            Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            if (this.gameObject.name == "BossBird")
            {
                victoryScreen.SetActive(true);
                GameObject theController = GameObject.Find("GameManager");
                menuController menuController = theController.GetComponent<menuController>();
                menuController.gameStart = false;
                
                Time.timeScale = 0f;
            }
            Destroy(gameObject);
            //Death sound
            
        }

    }

    public void TakeDamage(float damageTook)
    {
        currentHealth -= damageTook;
        gotHitSound.GetComponent<soundRandomizer>().PlayActive();


    }
}
