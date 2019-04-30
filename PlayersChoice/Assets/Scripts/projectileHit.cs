using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileHit : MonoBehaviour
{

    public Transform attackPos;
    public LayerMask whoIsEnemy;
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
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("Entered Enemy");
            GameObject thePlayer = GameObject.Find("Player");
            playerShoot playerShoot = thePlayer.GetComponent<playerShoot>();
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whoIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<enemyController>().TakeDamage(playerShoot.damageTook);
                Instantiate(hitEffect, this.transform.position, Quaternion.identity);
            }
        }
    }
}
