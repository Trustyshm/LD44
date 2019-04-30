using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class weaponMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    
    public float speed;

    private Animator anim;

    public float swordSwingtimeMax;
    public float swordSwingtime;

    public Image swordCooldown;

     
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        swordSwingtime = swordSwingtimeMax;
        
    }

    // Update is called once per frame
    void Update()
    {
        swordCooldown.fillAmount = swordSwingtime / swordSwingtimeMax;
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 10;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        

        if (swordSwingtime <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("Active");
                swordSwingtime = swordSwingtimeMax;

            }
        }
     

        if (Input.GetMouseButtonUp(0))
        {
            //anim.ResetTrigger("Active");
        }

        swordSwingtime -= Time.deltaTime;
    }
}
