using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    public AudioSource muse;
    void OnCollisionEnter(Collision objectInfo)
    {
        if (objectInfo.collider.tag == "person")
        {
            muse.Play();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
