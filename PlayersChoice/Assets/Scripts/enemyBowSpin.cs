using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBowSpin : MonoBehaviour
{
    public float speed;
    private Transform playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
        Vector2 direction = playerPosition.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 10;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}
