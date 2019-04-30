using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zSorting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpriteRenderer[] renderers = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer renderer in renderers)
        {
            if (renderer.name != "BackgroundGrass")
            {
                renderer.sortingOrder = (int)(renderer.transform.position.y * -100);
            }
        }
    }
}
