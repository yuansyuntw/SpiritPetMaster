using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpiritPetMaster
{
	public class FoodDetailPanel : MonoBehaviour {
			public GameObject food;
			public int ind;

			public void FoodDetailButton(GameObject _food)
			{
					food = _food;
					ind = _food.GetComponent<Food>().index;
					transform.Find("Text").GetComponent<Text>().text = _food.GetComponent<Food>().Description;
					transform.Find("FoodImage").GetComponent<Image>().sprite = _food.GetComponent<Food>().FoodSprite;
			}

			public void UseFood()
			{
				//if(food!=null && PlayerData.instance.GetFood(ind)>0)
				if(food!=null)
				{
					GameObject.FindGameObjectWithTag("FoodController").GetComponent<FoodController>().InstanceFood(ind);
				}
			}
	}
}