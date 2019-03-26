using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class opening : MonoBehaviour
{
    // Start is called before the first frame update
    public playerShake rockslide;
    void Start()
    {
        StartCoroutine(rockslide.Shake(4f,.01f));
        //transform.localPosition = new Vector3(-0.09f, 0.426f, 1.454f);
        SceneManager.LoadScene("battle2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
