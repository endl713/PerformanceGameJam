using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip myEvent;

    public AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            source.PlayOneShot(myEvent);
        }
    }


}
