﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyArrowHit : MonoBehaviour
{

    public Transform attackPos;
    public float attackRange;

    public float maxLifetime;
    private float lifetime;

    public GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        lifetime = maxLifetime;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("Entered Player With Arrow");
            GameObject thePlayer = GameObject.Find("Player");
            thePlayer.GetComponent<healthBar>().PlayerTakeBowDamage(healthBar.bowDamagePlayer);
            Instantiate(hitEffect, this.transform.position, Quaternion.identity);
            Destroy(gameObject);

        }
    }

}