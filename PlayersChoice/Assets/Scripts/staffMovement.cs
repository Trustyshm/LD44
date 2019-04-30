using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class staffMovement : MonoBehaviour
{
    private Vector3 mousePosition;

    public float speed;

    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Active");

        }

        if (Input.GetMouseButtonUp(0))
        {
            //anim.ResetTrigger("Active");
        }
    }
}