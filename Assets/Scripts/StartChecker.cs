using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartChecker : MonoBehaviour {

	static public StartChecker instance;
	public bool gameBegin = true;
	public bool gameStarted = false;
	public GameObject startMenu;
	
	void Awake(){
	
		SceneManager.sceneLoaded += OnSceneLoaded;
		if (StartChecker.instance == null)
		{
			StartChecker.instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			if (StartChecker.instance != this)
			{
				Destroy(gameObject);
			}
		}
	}

	void OnEnale(){
	}

	void Start(){
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode){
		GameObject temp = GameObject.FindWithTag("StartMenu");
		if(temp != null)
			startMenu = temp;
		if(startMenu!=null)
		{
			if(gameBegin){
				startMenu.SetActive(true);
				gameBegin = false;
			}
			else{
				startMenu.SetActive(false);
			}
		}
	}

	void Update(){
		// Debug.Log(gameStarting);
	}
	

	void OnDisable(){
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}
}
