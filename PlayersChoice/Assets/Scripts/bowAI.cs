using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bowAI : MonoBehaviour
{
    public float speed;
    private Transform target;

    private Vector2 moveSpot;


    private float waitTime;
    public float maxWaitTime;

    public float bowDistance;
    public int damagePlayer;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public Transform bowEnemy;

    public float minimumDistance;

    public LayerMask whoIsPlayer;

    public float followDistance;

    public float repulsionForce;

    public GameObject enemyArrow;


    public float maxShootTimer;
    private float shootTimer;

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



    // Start is called before the first frame update
    void Start()
    {
        waitTime = maxWaitTime;
        moveSpot = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Debug.Log(moveSpot);
        shootTimer = maxShootTimer;
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
        
        //Patrol if far
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

        //If close, follow to bow distance
        if (Vector2.Distance(transform.position, target.position) < followDistance && Vector2.Distance(transform.position, target.position) >= bowDistance)
        {
            anim.SetBool("EnemyMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            //   playerPosition = GameObject.FindWithTag("Player").transform;
            //   Vector2 direction = playerPosition.position - transform.position;
            //   float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 1;
            //   Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //   transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
        }


        // shoot if close enough
        if (Vector2.Distance(transform.position, target.position) < bowDistance && Vector2.Distance(transform.position, target.position) >= minimumDistance)
        {
            enemyMoving = false;
            anim.SetBool("EnemyMoving", false);
            playerPosition = GameObject.FindWithTag("Player").transform;
            Vector2 direction = playerPosition.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 30;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //   transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

            if (shootTimer <= 0)
            {
                //     Vector2 target = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                //     Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y + 0.1f);
                //     Vector2 direction = target - playerPosition;
                //     direction.Normalize();
                //     Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

                if (isRight == true)
                {
                    Vector2 arrowOrigin = new Vector2(transform.position.x + 0.2f, transform.position.y + 0.9f);
                    GameObject projectiles = (GameObject)Instantiate(enemyArrow, arrowOrigin, rotation);
                    projectiles.GetComponent<Rigidbody2D>().velocity = direction * speed;
                    shootTimer = maxShootTimer;
                }

                if (isRight == false)
                {
                    Vector2 arrowOrigin = new Vector2(transform.position.x + -0.4f, transform.position.y + 0.9f);
                    GameObject projectiles = (GameObject)Instantiate(enemyArrow, arrowOrigin, rotation);
                    projectiles.GetComponent<Rigidbody2D>().velocity = direction * speed;
                    shootTimer = maxShootTimer;
                }

            }
            //playerToDamage = GameObject.FindWithTag("Player");
            //Debug.Log(damagePlayer);
            //playerToDamage.GetComponent<healthBar>().PlayerTakeMeleeDamage(healthBar.meleeDamagePlayer);



            else
            {
                shootTimer -= Time.deltaTime;
            }


        }

        // run from player if too close
        if (Vector2.Distance(transform.position, target.position) <= minimumDistance)
        {
            //  playerPosition = GameObject.FindWithTag("Player").transform;
            //  Vector2 direction = playerPosition.position - transform.position;
            //  float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 30;
            //  Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //  transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
            anim.SetBool("EnemyMoving", true);
            transform.position = Vector2.MoveTowards(transform.position, -target.position*3, speed * Time.deltaTime);


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

        
        
    }
}


