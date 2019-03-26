using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderCollision : MonoBehaviour
{
    public Animator animator;
    public AudioSource kill;
    public AudioSource smash;
    void OnCollisionEnter(Collision spiderInfo)
    {
        if (spiderInfo.collider.tag == "weapon")
        {
            animator.SetFloat("health", 0);
            kill.Play();
        }
        if (spiderInfo.collider.tag == "rock")
        {
            animator.SetFloat("health", -10);
            smash.Play();
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
