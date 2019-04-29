using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // Public components
    public AudioClip[] soundsFX;

    // Private components
    private AudioSource audioM;

    private void Awake()
    {
        audioM = GetComponent<AudioSource>();
    }
    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        
    }
    public void PlaySFX(int soundToPlay)
    {
        if (soundToPlay < soundsFX.Length)
        {
            audioM.PlayOneShot(soundsFX[soundToPlay]);
        }
    }
}
