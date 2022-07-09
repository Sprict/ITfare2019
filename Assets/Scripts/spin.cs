using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class spin : MonoBehaviour
{
    public bool isSpin = false;
    [SerializeField]
    private float spinSpeed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        if(isSpin)
         rb.angularVelocity = spinSpeed;
    }
}
