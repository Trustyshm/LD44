using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class bossSpawn : MonoBehaviour
{
    public TextMeshProUGUI enemyText;
    private int enemyAmount;
    public GameObject bossObject;

    public AudioSource bossTheme;

    private bool bossSpawned;

    public AudioSource backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        bossSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] targetGOs = GameObject.FindGameObjectsWithTag("Enemy");
        if (targetGOs.Length == 0)
        {
            bossObject.SetActive(true);
            enemyText.text = "Boss Spawned";
            backgroundMusic.Stop();
            bossTheme.Play();
            bossSpawned = true;
        }

        if (bossSpawned == false)
        {
            enemyAmount = targetGOs.Length;
            enemyText.text = enemyAmount.ToString();
        }


    }
}
