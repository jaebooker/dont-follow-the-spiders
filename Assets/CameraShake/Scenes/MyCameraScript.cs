using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Example for how to add the shake to your camera script.
/// </summary>
public class MyCameraScript : MonoBehaviour {
	Quaternion rot;
	Vector3 pos;
	
	void Awake() {
		rot = transform.rotation;
		pos = transform.position;
	}
	
	void Update () {
		// A camera manager sets the position and the rotation.
		transform.rotation = rot;
		transform.position = pos;
		
          
		// After this just set the shake position and rotation changes.
		// If your camera script modifies the rotation and position in LateUpdate, 
		// then move this to LateUpdate after changing these attributes.
		// This call add a shake offset to the camera position and rotation, that the reason why this works.
		Metadesc.CameraShake.ShakeResult shakeResult = 
			Metadesc.CameraShake.ShakeManager.I.UpdateAndGetShakeResult();
		if (shakeResult.DoProcessShake) {
			transform.localPosition += shakeResult.ShakeLocalPos;
			transform.localRotation *= shakeResult.ShakeLocalRot;
		}
	}
}
