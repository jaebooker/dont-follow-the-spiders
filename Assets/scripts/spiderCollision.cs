using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderCollision : MonoBehaviour
{
    public Animator animator;
    public AudioSource kill;
    void OnCollisionEnter(Collision spiderInfo)
    {
        if (spiderInfo.collider.tag == "weapon")
        {
            Debug.Log("They were singing, bye, bye, mr spider guy");
            animator.SetFloat("health", 0);
            kill.Play();
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
