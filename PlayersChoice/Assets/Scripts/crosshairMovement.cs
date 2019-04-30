using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crosshairMovement : MonoBehaviour
{
    private Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 5f;
        this.transform.position = mousePosition;
    }
}
