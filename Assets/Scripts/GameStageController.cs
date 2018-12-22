using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStageController : MonoBehaviour {

    public int Gameover;
    public Text Stage;
    public int stop;
    public int Killnum;

    public int ExpPerMonster = 10;
    public int ExpBoss = 200;

    public string dropPetKind;
	public float getPetPersentage = 20;
	public int[] foodsCounter = new int[SpiritPetMaster.FoodController.foodsNumber];

    private bool stageEnd = false;
    public bool win = false;

    public GameObject petMoodBuff;
    private float startTimer;
    private bool buffed = false;
    public float waitTime = 0.25f;

    public GameObject resultsPanel;


    private float timergameover = 0;

	// Use this for initialization
	void Start () {
        Gameover = 0;
        stop = 0;
        Killnum = 0;
        Stage.text = "";
        startTimer = Time.time;
    }

    // Update is called once per frame
    void Update() {
        if(!buffed && (Time.time-startTimer>waitTime))
        {
            petMoodBuff.SetActive(true);
            buffed = true;
        }


        if(!stageEnd)
        {
            if (Gameover == 1)
            {
                stop = 1;
                Stage.text = "Win";
                stageEnd = true;
                win = true;
                stageWinRewords();
            } 
            else if (Gameover == 2) {
                stop = 1;
                Stage.text = "Lose";
                stageEnd = true;
                win = false;
                stageLoseRewords();
            }
        }
        else if (Gameover != 0 && Gameover!=-1)
        {
            timergameover += Time.deltaTime;
            if (timergameover > waitTime)
            {
                resultsPanel.SetActive(true);
                Gameover = -1;
            }
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

    private void stageWinRewords(){
        for(int i=0; i<foodsCounter.Length; ++i)
        {
            foodsCounter[i] += Random.Range(0,3);
        }
    }
    private void stageLoseRewords(){
        for(int i=0; i<foodsCounter.Length; ++i)
        {
            foodsCounter[i] += (Random.Range(0,100)>70)?1:0;
        }
    }
}
