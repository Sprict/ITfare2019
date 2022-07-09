using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip startSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] AudioClip dururu;
    [SerializeField] AudioClip cheer;

    public void playStartSound()
    {
        audioSource.PlayOneShot(startSound, .5f);
    }

    public void playFinishSound()
    {
        audioSource.PlayOneShot(finishSound);
    }

    public void playDururuSound()
    {
        audioSource.PlayOneShot(dururu);
    }

    public void playCheerSound()
    {
        audioSource.PlayOneShot(cheer);
    }
}
