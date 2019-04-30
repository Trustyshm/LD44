using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closestEnemy : MonoBehaviour
{

    public float speed;
    private Transform enemyPosition;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        FindClosestEnemy();






    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
                Vector2 directions = closest.transform.position - transform.position;
                float angles = Mathf.Atan2(directions.y, directions.x) * Mathf.Rad2Deg - 90;
                Quaternion rotations = Quaternion.AngleAxis(angles, Vector3.forward);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotations, speed * Time.deltaTime);
            }
        }
        return closest;
    }







}
