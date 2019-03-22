using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScroller : MonoBehaviour {
	public float scrollSpeed = 0.1f;

	// Update is called once per frame
	void Update () {
		if(GetComponent<Renderer>().material.shader.isSupported)
			Camera.main.depthTextureMode |= DepthTextureMode.Depth;

		float offset = Time.time * scrollSpeed;
		//Texture scrolling is instanced separately | Best if your scene contains multiple water planes of different speeds
		//GetComponent.<Renderer>().material.SetTextureOffset ("_MainTex", Vector2(offset/10.0, offset));

		//Share texture scrolling among objects with the same material | Best if your scene contains a single water plane, or multiple water of the same speed
		Vector2 k = new Vector2(offset / 10.0f, offset);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", k);
	}
}
