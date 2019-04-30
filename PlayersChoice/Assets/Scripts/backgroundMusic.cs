using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundMusic : MonoBehaviour
{
    private bool nextSong;
    private AudioSource thisAudio;
    // Start is called before the first frame update
    void Start()
    {
        thisAudio = GetComponent<AudioSource>();
        nextSong = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (nextSong == true)
        {
           GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<continuousBackground>().StopMusic();
           thisAudio.Play();
           Debug.Log(thisAudio.name);
           nextSong = false;

        }

    }

    public void GameIsStarted()
    {
        nextSong = true;
    }
}
