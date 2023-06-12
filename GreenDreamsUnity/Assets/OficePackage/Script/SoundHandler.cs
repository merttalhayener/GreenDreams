using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandler : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void TypingSound()
    {
        Debug.Log("Ses çaðýrdý");
        source.PlayOneShot(clip);
    }
}
