using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour {

    public int Type;//water 1 wind 2
    public GameObject Player;
    public int BoomNum;
    //public Slider PlayerO2;
    private float O2, timer;

    void Start () {
        BoomNum = 0;
        /*if (Type == 1)
        {
            O2 = 100;
            PlayerO2.gameObject.SetActive(true);
            Player.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            Player.GetComponent<Pet01_Controller>().force = 100f;
        }
        else
        {
            //Player.GetComponent<Pet01_Controller>().force = 500f;
        }*/
    }
	
	void Update () {
		if(Type == 1)
        {
            Player.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
            Player.GetComponent<Pet01_Controller>().force = 200f;
            /*timer += Time.deltaTime;
            PlayerO2.value = O2 / 100;
            if(timer > 1)
            {
                if (Player.transform.position.y < 4f && O2 >= 0) O2 -= 3;
                else if (Player.transform.position.y > 4f && O2 + 5 < 100) O2 += 5;
                else if (O2 <= 0) Player.GetComponent<Pet01_Controller>().HP -= 3;
                timer = 0;
            }
            if (Player.transform.position.y < 4f)
            {
                Player.GetComponent<Rigidbody2D>().gravityScale = 0.1f;
                Player.GetComponent<Pet01_Controller>().force = 100f;
            }
            else if (Player.transform.position.y > 4f)
            {
                Player.GetComponent<Rigidbody2D>().gravityScale = 1f;
                Player.GetComponent<Pet01_Controller>().force = 250f;
            }
            */
        }
	}
}
