using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;
    public Rigidbody rb;
    void Start()
    {
        rb.velocity = transform.forward * -speed/4;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * -speed/4;
    }
}
