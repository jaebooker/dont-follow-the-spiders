using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Cat_Lite_Demo : MonoBehaviour {

	public Text camRotateCheck;
	bool camRotate = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(camRotate)
			Camera.main.transform.RotateAround (Vector3.zero, Vector3.up, 40 * Time.deltaTime);
	}

	public void CamRotate(){
		if (camRotate) {
			camRotate = false;
			camRotateCheck.gameObject.SetActive (false);
		} 
		else {
			camRotate = true;
			camRotateCheck.gameObject.SetActive (true);
		}

	}
}
