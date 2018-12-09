using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStageController : MonoBehaviour {

    public int Gameover;
    public Text Stage;
    public int stop;

    private float timergameover = 0;

	// Use this for initialization
	void Start () {
        Gameover = 0;
        stop = 0;
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

    public void ClickStopButtom()
    {
        GameObject button = GameObject.Find("StopButton");
        if (stop == 0)
        {
            stop = 1;
            button.GetComponentInChildren<Text>().text = "Run";
            /*ColorBlock colors = button.GetComponent<Button>().colors;
            colors.normalColor = new Color(100, 180, 235);
            colors.highlightedColor = new Color(100, 180, 235);
            button.GetComponent<Button>().colors = colors;*/
        }
        else
        {
            stop = 0;
            button.GetComponentInChildren<Text>().text = "Stop";
            /*ColorBlock colors = button.GetComponent<Button>().colors;
            colors.normalColor = new Color(200, 200, 200);
            colors.highlightedColor = new Color(200, 200, 200);
            button.GetComponent<Button>().colors = colors;*/
        }
    }
}
