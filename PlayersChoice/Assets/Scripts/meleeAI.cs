using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAI : MonoBehaviour
{
    public float speed;
    public Transform target;

    private Vector2 moveSpot;


    private float waitTime;
    public float maxWaitTime;

    public float meleeDistance;
    public int damagePlayer;

    public float minX; 
    public float maxX; 
    public float minY; 
    public float maxY;

    public Transform meleeEnemy;
    public float attackRange;
    public LayerMask whoIsPlayer;

    public float followDistance;

    public float repulsionForce;

    private GameObject[] enemyPosition;

    private Vector2 avoidanceForce;
    private Rigidbody2D rb;



    public float maxSwingTimer;
    private float swingTimer;

    private Transform playerPosition;

   public float rotateSpeed;

    private bool isRight;

    public bool enemyMoving;

    private Animator anim;

    public bool clothEnemy;
    public bool redEnemy;
    public bool blueEnemy;

    public bool greenHair;
    public bool blackHair;
    public bool redHair;

    public GameObject spriteMask;

    public GameObject redHairSprite;
    public GameObject blackHairSprite;
    public GameObject greenHairSprite;

    public AnimationClip swing;

    public GameObject enemySword;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = maxWaitTime;
        moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Debug.Log(moveSpot);
        swingTimer = maxSwingTimer;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (clothEnemy == true)
        {
            anim.SetBool("ClothEnemy", true);
            
        }
        if (clothEnemy == false)
        {
            anim.SetBool("ClothEnemy", false);
        }
        if (blueEnemy == true)
        {
            anim.SetBool("BlueEnemy", true);
        }
        if (blueEnemy == false)
        {
            anim.SetBool("BlueEnemy", false);
        }
        if (redEnemy == true)
        {
            anim.SetBool("RedEnemy", true);
        }
        if (redEnemy == false)
        {
            anim.SetBool("RedEnemy", false);
        }

        if (redHair == true)
        {
            redHairSprite.SetActive(true);
            blackHairSprite.SetActive(false);
            greenHairSprite.SetActive(false);
            spriteMask.SetActive(true);

        }
        if (blackHair == true)
        {
            redHairSprite.SetActive(false);
            blackHairSprite.SetActive(true);
            greenHairSprite.SetActive(false);
            spriteMask.SetActive(true);
        }

        if (greenHair == true)
        {
            redHairSprite.SetActive(false);
            blackHairSprite.SetActive(false);
            greenHairSprite.SetActive(true);
            spriteMask.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Vector2.Distance(transform.position, target.position) >= followDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, moveSpot) < 0.5f)
            {
                if (waitTime <= 0)
                {
                    moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    waitTime = maxWaitTime;
                    Debug.Log(moveSpot);
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }

        
        if (Vector2.Distance(transform.position, target.position) < followDistance && Vector2.Distance(transform.position, target.position) >= 1.0f )
        {
            anim.SetBool("EnemyMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

       

        if (Vector2.Distance(transform.position, target.position) < meleeDistance)
        {
            anim.SetBool("EnemyMoving", false);

            if (swingTimer <= 0)
            {
                Collider2D playerToDamage = Physics2D.OverlapCircle(meleeEnemy.position, attackRange, whoIsPlayer);

                enemySword.GetComponent<Animator>().SetTrigger("SwungSword");
                  
                playerToDamage.GetComponent<healthBar>().PlayerTakeMeleeDamage(healthBar.meleeDamagePlayer);
                
                swingTimer = maxSwingTimer;
            }

            else
            {
                swingTimer -= Time.deltaTime;
            }
            
            
        }
        playerPosition = GameObject.FindWithTag("Player").transform;
        if (playerPosition.transform.position.x >= transform.position.x)
        {
            isRight = true;
        }

        if (playerPosition.transform.position.x < transform.position.x)
        {
            isRight = false;
        }



        if (isRight == true)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        if (isRight == false)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);

        }


        // run from player if too close
        if (Vector2.Distance(transform.position, target.position) < 1.0f)
        {

            anim.SetBool("EnemyMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, -target.position * 2, speed * Time.deltaTime);


        }



    }

    void OnCollisionEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log("AAAAAAAAAA" + collider.gameObject.name);
            Vector3 dir = collider.gameObject.transform.position - transform.position;
            dir = -dir.normalized;
            Debug.Log(dir);
            GetComponent<Rigidbody2D>().AddForce(dir * 2000);
            transform.position = Vector2.MoveTowards(transform.position, -target.position, speed * Time.deltaTime);

        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeEnemy.position, attackRange);
    }
}
