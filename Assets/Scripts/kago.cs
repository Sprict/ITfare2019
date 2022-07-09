using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kago : MonoBehaviour
{
    public float rate = 1.0f;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //this.transform.position = new Vector3(7.0f * Mathf.Sin(rate * time), -7.0f, 0);
        this.transform.position = new Vector3(Mathf.PingPong(2.0f * time, 16.0f) - 8.0f, -7.0f, 0);
        time += Time.deltaTime;
    }


}