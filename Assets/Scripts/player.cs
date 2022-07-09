using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSource;
    public float t = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
    }
}
