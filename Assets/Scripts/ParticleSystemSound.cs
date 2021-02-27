using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSound : MonoBehaviour
{
    private ParticleSystem particleSystem;

    private AudioSource audioSource;
    [SerializeField]private AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(particleSystem.particleCount > 0)
        {
            if (audioSource.isPlaying)
                return;
            audioSource.PlayOneShot(audioClip);
        }
    }
}
