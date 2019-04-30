using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundRandomizer : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;
    private AudioClip clip;
    private int clipSelectorInt;



    private float clipCountdown;
    private float clipSelector;

    // Start is called before the first frame update
    void Start()
    {
        clipCountdown = 1f;
        clipSelector = 0;

    }

    // Update is called once per frame
    void Update()
    {
        clipCountdown -= Time.deltaTime;
        if (clipCountdown <= 0)
        {

            SoundRandomizer();
            PlayClip();

        }


    }

    void PlayClip()
    {
        clipSelectorInt = Mathf.RoundToInt(clipSelector);
        clip = clips[clipSelectorInt];
        source.clip = clip;

    }

    void SoundRandomizer()
    {
        clipCountdown = 1f;
        clipSelector = Random.Range(0, clips.Length);
    }

    public void PlayActive()
    {
        source.PlayOneShot(source.clip);

    }

}



