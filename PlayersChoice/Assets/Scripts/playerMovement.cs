using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    private Animator anim;
    private bool playerMoving;
    private Vector2 lastAction;
    private Rigidbody2D body;
    private float moveSpeed;
    private bool isSprinting;
    public float walkSpeed;
    public float sprintSpeed;

    public bool canSprint;
    public float maxSprint;
    private float sprintTime;

    public Image energyBar;

    private bool isRight;

    private float normalizedSprint;

    private Vector3 mousePosition;


    private Vector3 tempX;



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isSprinting = false;
        sprintTime = maxSprint;
    }

    // Update is called once per frame
    void Update()
    {
        energyBar.fillAmount = sprintTime/maxSprint;
        tempX = transform.position;
        tempX.x = Mathf.Clamp(transform.position.x,-47,45);
        tempX.y = Mathf.Clamp(transform.position.y, -24, 23);
        transform.position = tempX;

        


        if (canSprint == true)
        {
            
            if (sprintTime > maxSprint)
            {
                sprintTime = maxSprint;
            }

            if (sprintTime > 0)
            {
                if (Input.GetButton("Sprint"))
                {
                    isSprinting = true;
                    Debug.Log("sprinting");
                }

                if (Input.GetButtonUp("Sprint"))
                {
                    isSprinting = false;
                    Debug.Log("done sprinting");
                    sprintTime += 0.2f * Time.deltaTime;
                }
            }

            if (sprintTime <= 0)
            {
                moveSpeed = walkSpeed;
                isSprinting = false;
                sprintTime = 0;
            }

            if (sprintTime != maxSprint && isSprinting == false)
            {
                sprintTime += 0.2f * Time.deltaTime;
            }





        }
        

        if (isSprinting == true)
        {
            moveSpeed = sprintSpeed;
            sprintTime -= Time.deltaTime;
        }

        if (isSprinting == false)
        {
            moveSpeed = walkSpeed;
            sprintTime += 0.2f * Time.deltaTime;
        }

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /*
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f && mousePosition.x > transform.position.x)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastAction = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f && mousePosition.x < transform.position.x)
        {
            transform.Translate(-new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastAction = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        } */

        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            if (mousePosition.x > transform.position.x)
            {
                transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                playerMoving = true;
                lastAction = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }
            if (mousePosition.x < transform.position.x)
            {
                transform.Translate(-new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                playerMoving = true;
                lastAction = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }
            
        }

        /*
        if (Input.GetAxisRaw("Horizontal") > 0.5f)
        {
            //isRight = true;
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastAction = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }

        if (Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            //isRight = false;
            transform.Translate(new Vector3(-Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastAction = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        }
        */
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            playerMoving = true;
            lastAction = new Vector2(Input.GetAxisRaw("Vertical"), 0f);
        }
        if (Input.GetAxisRaw("Vertical") == 0f && Input.GetAxisRaw("Horizontal") == 0f)
        {
            playerMoving = false;
        }

        

        if (mousePosition.x >= transform.position.x)
        {
            isRight = true;
        }

        if (mousePosition.x < transform.position.x)
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

        // anim.SetFloat("XMovement", Input.GetAxisRaw("Horizontal"));
        // anim.SetFloat("YMovement", Input.GetAxisRaw("Vertical"));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetBool("PlayerSprinting", isSprinting);
       // anim.SetFloat("LastActionX", lastAction.x);
       // anim.SetFloat("LastActionY", lastAction.y);
    }
}
