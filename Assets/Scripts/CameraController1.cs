using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController1 : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;
    public float rightmost, leftmost;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 postPostion = transform.position;
        transform.position = player.transform.position + offset;
        //Debug.Log(player.transform.position);
        //if (transform.position.x > rightmost || transform.position.x < leftmost) transform.position = postPostion + new Vector3(0, transform.position.y - postPostion.y, 0);
        
    }

}
