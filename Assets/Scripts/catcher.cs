using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class catcher : MonoBehaviour
{
    private int _localScore = 0;
    public int localScore { get; set; }
    [SerializeField] private int apple_point = 5;
    [SerializeField] private int orange_point = 10;
    [SerializeField] private int greap_point = 15;
    [SerializeField] private int banana_point = 50;
    [SerializeField] GameObject pointEffect;

    [SerializeField]
    private Timer timer;

    [SerializeField]
    private AudioSource audioSource = null;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameState.state != GameState.statusList.Started)
        {
            return;
        }

        int point = 0;
        switch (other.gameObject.name)
        {
            case "apple(Clone)": point += apple_point; break;
            case "orange(Clone)": point += orange_point; break;
            case "greap(Clone)": point += greap_point; break;
            case "banana(Clone)": point += banana_point;  break;
        }
        localScore += point;
        // pointEffect表示
        GameObject instance = Instantiate(pointEffect, transform.GetChild(0));
        instance.GetComponent<Text>().text = "+" + Convert.ToString(point);
        audioSource.Play();
    }
}
