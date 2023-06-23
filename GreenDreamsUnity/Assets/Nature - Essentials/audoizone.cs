using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audoizone : MonoBehaviour
{
    public AudioSource audioSource;
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Selectable" && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
