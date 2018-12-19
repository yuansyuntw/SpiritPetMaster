using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
	public class BattleResults : MonoBehaviour {

		private GameObject playerData;
        public string PetKind;
		private int[] foodsCounter = new int[FoodController.foodsNumber];
		

		// Use this for initialization
		void Start () {
			playerData = GameObject.FindGameObjectWithTag("PlayerData");
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void SetToPlayer()
		{
			for(int i=0;i<FoodController.foodsNumber;++i)
			{
				PlayerData.instance.AddFood(i, foodsCounter[i]);
			}
		}
	}
}