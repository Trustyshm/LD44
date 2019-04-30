using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
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


            Instantiate(deathEffect, this.transform.position, Quaternion.identity);
            victoryScreen.SetActive(true);
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
