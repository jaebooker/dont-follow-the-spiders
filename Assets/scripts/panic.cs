using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class panic : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
		if (Input.GetButtonDown("Fire1")) {
			Debug.Log("asfegweg");
			SceneManager.LoadScene("peace");
		}
		if (OVRInput.Get (OVRInput.Button.One)) {
			Debug.Log("asfegweg");
			SceneManager.LoadScene("peace");
		}
    }
}
