using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetMoodBuff : MonoBehaviour {

	private float startTime;
	public float showingTime = 3f;

	public Pet01_Controller pet;
	public float maxBuffMultiplier = 1.5f;
	public float minBuffMultiplier = 0.75f;
	private float multiplier = 1f;

	public Image petImage;
	public Text text;

	void Start () {
		startTime = Time.time;
		string petSpriteName = pet.Kind.ToString();
		if(pet.Mood<100f/4f)
		{
			petSpriteName += "/Damaged/damaged_3";
			multiplier = pet.Mood / 100f/4f * (1-minBuffMultiplier) + minBuffMultiplier;
			text.text = pet.Name + " 心情不好、能力值下降了";
		}
		else if(pet.Mood>(100f-100f/4f))
		{
			petSpriteName += "/Happy/happy_3";
			multiplier = 1 + (pet.Mood-100f/4f) / (100f-100f/4f) * (maxBuffMultiplier-1);
			text.text = pet.Name + " 狀態絕佳，獲得了全能力上升";
		}
		gameObject.SetActive(true);
		if (pet.Kind.ToString() != "")
		{
			var img = Resources.Load(petSpriteName, typeof(Sprite));
			if (img != null)
			{
				petImage.sprite = (Sprite)img;
			}
		}
		pet.Buff(multiplier);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Time.time-startTime > showingTime)
		{
			Destroy(gameObject);
		}
	}
}
