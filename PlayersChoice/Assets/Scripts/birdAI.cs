using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdAI : MonoBehaviour
{
    //Movement SPeed
    public float speed;

    //Projectile Speed;
    public float projectileSpeed;

    //Target player
    private Transform target;


    //Patrol Spots
    private Vector2 moveSpot;

    //Wait between Patrol
    private float waitTime;
    public float maxWaitTime;


    //Distance at which to hit player
    public float bowDistance;


    //Patrol constraints
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;


    //Shoot Origin
    public Transform bowEnemy;


    //Move away if too close
    public float minimumDistance;

    public LayerMask whoIsPlayer;

    //Aggro Range
    public float followDistance;


    //Projectile to shoot
    public GameObject enemyArrow;

    //Shoot cooldown variables
    public float maxShootTimer;
    private float shootTimer;

    //PLayers Position
    private Transform playerPosition;

    //Track Player
    private bool isRight;


    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        waitTime = maxWaitTime;
        moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Debug.Log(moveSpot);
        shootTimer = maxShootTimer;
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
    
        //Patrol if far
        if (Vector2.Distance(transform.position, target.position) > followDistance)
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
        
        //Aggro if close
        if (Vector2.Distance(transform.position, target.position) < followDistance && Vector2.Distance(transform.position, target.position) >= bowDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        // shoot if close enough
        if (Vector2.Distance(transform.position, target.position) < bowDistance && Vector2.Distance(transform.position, target.position) >= minimumDistance)
        {
            playerPosition = GameObject.FindWithTag("Player").transform;
            Vector2 direction = playerPosition.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 30;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            if (shootTimer <= 0)
            {
                if (isRight == true)
                {
                    // Vector2 arrowOrigin = new Vector2(transform.position.x + 0.2f, transform.position.y + 0.9f);
                    GameObject projectiles = (GameObject)Instantiate(enemyArrow, bowEnemy.transform.position, rotation);
                    projectiles.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                    Debug.Log("Projectile 1");
                    shootTimer = maxShootTimer;
                }

                if (isRight == false)
                {
                    //Vector2 arrowOrigin = new Vector2(transform.position.x + -0.4f, transform.position.y + 0.9f);
                    GameObject projectiles = (GameObject)Instantiate(enemyArrow, bowEnemy.transform.position, rotation);
                    projectiles.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
                    Debug.Log("Projectile 2");
                    shootTimer = maxShootTimer;
                }

            }
            if (shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
            }


        }
        // run from player if too close
        if (Vector2.Distance(transform.position, target.position) <= minimumDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, -target.position * 3, speed * Time.deltaTime);
        }

        //Face Player
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
        

    }
}
