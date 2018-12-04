using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour {

    [Header("Camera region")]
	public float minX=0;
	public float maxX=0;
	public float minY=0;
	public float maxY=0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(Mathf.Clamp( transform.position.x , minX, maxX), Mathf.Clamp( transform.position.y , minY, maxY), 0);
	}
}
