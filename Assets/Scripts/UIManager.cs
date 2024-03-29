﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider timeSpeedSlider = null;
    [SerializeField]
    private Text timeSpeedSliderLabel = null;
    [SerializeField]
    private Slider noiseScaleSlider = null;
    [SerializeField]
    private Text noiseScaleSliderLabel = null;
    [SerializeField]
    private Material deformationMaterial = null; //変形まてりある

    private bool useSampleFromAudio = false;
    private SpectrumManager spectrumManager = null;
    private float sampleMultipler = 100.0f;
    [SerializeField]
    private float delayBy = 1.0f;
    private float elapsedTime = 0; //経過時間
    void Start()
    {
        spectrumManager = SpectrumManager.Instance;
        timeSpeedSlider.onValueChanged.AddListener((value) => timeSpeedSliderChanged(value));
        noiseScaleSlider.onValueChanged.AddListener((value) => noiseScaleSliderChanged(value));
        
    }

    // Update is called once per frame
    void Update()
    {

        if(deformationMaterial != null)
        {
            if (useSampleFromAudio)
            {
                float value = spectrumManager.Samples[0] * sampleMultipler;
                float clampValue = Mathf.Clamp(value, 0.1f, 100.0f);

                if(elapsedTime >= delayBy)
                {
                    //timeSpeedSliderChanged(clampValue);
                    noiseScaleSliderChanged(clampValue);
                    elapsedTime = 0;
                }
                else
                {
                    elapsedTime += Time.deltaTime * 1.0f;
                    Debug.Log(elapsedTime);
                }
            }
        }

    }

    void timeSpeedSliderChanged(float newValue)
    {
        timeSpeedSlider.value = newValue;
        deformationMaterial.SetFloat("_TimeSpeed", timeSpeedSlider.value);
        timeSpeedSliderLabel.text = $"Time Speed: {timeSpeedSlider.value}";
    }

    void noiseScaleSliderChanged(float newValue)
    {
        noiseScaleSlider.value = newValue;
        deformationMaterial.SetFloat("_NoiseScale", noiseScaleSlider.value);
        noiseScaleSliderLabel.text = $"Noise Scale: {noiseScaleSlider.value}";
    }
}
