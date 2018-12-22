using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartChecker : MonoBehaviour {

	static StartChecker instance;
	public bool gameStarting = true;
	public GameObject startMenu;
	
	void Awake(){
	
    	SceneManager.sceneLoaded += OnSceneLoaded;
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			if (instance != this)
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
			if(gameStarting){
				startMenu.SetActive(true);
				gameStarting = false;
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
