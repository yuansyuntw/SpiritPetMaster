using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private float camera_Z;
	public float originalSize;
	public float zoomInSize;
	public float speed = 1.0f;

	private bool moveLeft = false;
	private bool moveRight = false;

	// public GameObject CameraRegion;
	public GameObject gameRegionObject;
	private GameRegion gameRegion;

	// Use this for initialization
	void Start () {
		// originalSize = Camera.main.orthographicSize;
		camera_Z = transform.position.z;
		gameRegion = gameRegionObject.GetComponent<GameRegion>();
		// RegionMinX = CameraRegion.transform.position.x - CameraRegion.GetComponent<SpriteRenderer>().size.x/2 + Camera.main.sensorSize.x;
		// RegionMaxX = CameraRegion.transform.position.x + CameraRegion.GetComponent<SpriteRenderer>().size.x/2 - Camera.main.sensorSize.x;
		// RegionMinY = CameraRegion.transform.position.y - CameraRegion.GetComponent<SpriteRenderer>().size.y/2 + Camera.main.sensorSize.y;
		// RegionMaxY = CameraRegion.transform.position.y + CameraRegion.GetComponent<SpriteRenderer>().size.y/2 - Camera.main.sensorSize.y;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(Mathf.Clamp( transform.position.x , gameRegion.RegionMinX, gameRegion.RegionMaxX) ,Mathf.Clamp( transform.position.y , gameRegion.RegionMinY, gameRegion.RegionMaxY), camera_Z);
        SpiritPetMaster.PetViewController.instance.FocusingPetView();
	}

	void FixedUpdate(){
		if(moveLeft)
			transform.position += new Vector3(-speed*Time.deltaTime, 0, 0);
		if(moveRight)
			transform.position += new Vector3(speed*Time.deltaTime, 0, 0);
	}

	public void LeftButtonDown(){
		moveLeft = true;
	}
	public void LeftButtonUp(){
		moveLeft = false;
	}
	public void RightButtonDown(){
		moveRight = true;
	}
	public void RightButtonUp(){
		moveRight = false;
	}

	public void FocusView(){
		Camera.main.orthographicSize = zoomInSize;
	}
	public void DefaultView(){
		Camera.main.orthographicSize = originalSize;
	}
}
