using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStageController : MonoBehaviour {

    public int Gameover;
    public Text Stage;

    private float timergameover = 0;

	// Use this for initialization
	void Start () {
        Gameover = 0;
        Stage.text = "";
    }

    // Update is called once per frame
    void Update() {
        if (Gameover == 1)
        {
            Stage.text = "Win";
        } 
        else if (Gameover == 2) {
            Stage.text = "Lose";
        }
        if (Gameover != 0)
        {
            timergameover += Time.deltaTime;
        }

        if (timergameover > 5f)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("InteractiveScene");
        }
    }
}
