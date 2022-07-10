using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinMove : MonoBehaviour
{
    public bool isMove = true;
    public float amp = 1.0f;
    public float rate = 1.0f;
    private float origintime = 0;
    private Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    void Update()
    {
        if (isMove)
        {
            transform.position = initPos + new Vector3(amp * Mathf.Sin(rate * origintime), 0, 0);
            origintime += Time.deltaTime;
        }

    }
}
