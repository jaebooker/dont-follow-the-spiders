using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiderpeacock : MonoBehaviour
{
    // Start is called before the first frame update
    public float peacocke;
    public Animator peacock;
    void Start()
    {
        peacocke = 1;
    }

    // Update is called once per frame
    void Update()
    {
        peacock.SetFloat("coconut", peacocke);
    }
}
