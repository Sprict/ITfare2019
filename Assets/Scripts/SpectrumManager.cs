using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Core;

public class SpectrumManager : Singleton<SpectrumManager>
{
    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField]
    private float[] samples = new float[512];

    public float[] Samples
    {
        get
        {
            return this.samples;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() => audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
}
