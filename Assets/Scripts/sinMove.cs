using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinMove : MonoBehaviour
{
    public bool isMove = false;
    public float amp = 2.0f;
    public float rate = 1.0f;
    private float time = 0;

    void Update()
    {
        if (isMove)
        {
            transform.position = new Vector3(amp * Mathf.Sin(rate * time), transform.position.y, 0);
            time += Time.deltaTime;
        }

    }
}
