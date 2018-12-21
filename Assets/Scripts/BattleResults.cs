using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpiritPetMaster
{
	public class BattleResults : MonoBehaviour {

		public Pet pet;
		public GameStageController gameStage;
		public GameObject newPetPanel;
		public float getPetPersentage = 20;



		public GameObject WinTitle;
		public GameObject LoseTitle;

		public GameObject ItemPrefab;
		public RectTransform ItemPrefabSize;
		public RectTransform ItemContainer;
		private float gapWidth = 50;
		private float gapHeight = 50;
		public int itemEachLine = 3;

		public Text detail;

		public const float MoodChanged = 3;

		void Start(){
		}

		// Use this for initialization
		void OnEnable () {
			//pet.GetComponent<Collider2D>().isTrigger = true;
			gapWidth = (ItemContainer.sizeDelta.x - ItemPrefabSize.sizeDelta.x*itemEachLine) / (itemEachLine+1);
			gapHeight = ItemContainer.sizeDelta.y - ItemPrefabSize.sizeDelta.y*2;
			// Debug.Log(gapWidth);
			// Debug.Log(gapHeight);
			ResetAllThing();
			if(gameStage.Gameover == 1)
				WinTitle.SetActive(true);
			else
				LoseTitle.SetActive(true);
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		public void ResetAllThing()
		{
			WinTitle.SetActive(false);
			LoseTitle.SetActive(false);
			ClearOldItems();
			SetupItems();
		}


		public void ClearOldItems()
		{
			// GameObject itemList = transform.Find("ItemContainer").gameObject;
			if(ItemContainer!=null)
			{
				foreach(Transform oldItem in ItemContainer)
				{
					Destroy(oldItem.gameObject);
				}
			}
		}

		private void BuildItem(int ind, int num, Sprite img)
		{
			// GameObject itemList = transform.Find("ItemContainer").gameObject;
			GameObject item = Instantiate(ItemPrefab, Vector2.zero, Quaternion.identity, ItemContainer.gameObject.transform);
			Vector2 posDelta = new Vector2(ItemPrefabSize.sizeDelta.x/2,ItemPrefabSize.sizeDelta.y/2);
			Vector2 _pos = new Vector2(gapWidth + posDelta.x + (ItemPrefabSize.sizeDelta.x+gapWidth)*(int)(ind%itemEachLine), - posDelta.y - (ItemPrefabSize.sizeDelta.y+gapHeight)*(int)(ind/itemEachLine));
			item.transform.localPosition = _pos;
			item.GetComponentInChildren<Image>().sprite = img;
			item.GetComponentInChildren<Text>().text = "× "+num.ToString();
			item.SetActive(true);
		}
		public void SetupItems()
		{
			int counter = 0;
			for(int i=0; i<gameStage.foodsCounter.Length;++i)
			{
				if(gameStage.foodsCounter[i]>0)
				{
					var img = Resources.Load("Foods/Food"+i.ToString(), typeof(Sprite));
					BuildItem(counter, gameStage.foodsCounter[i], (Sprite)img);
					counter+=1;
				}
				string resultsDetail = "";
				resultsDetail += pet.Name;

				resultsDetail += "擊敗了 ";
				resultsDetail += gameStage.Killnum.ToString();
				resultsDetail += " 隻怪物";
				if(gameStage.win)
					resultsDetail += "，還打敗了BOSS";
				resultsDetail += "\n\n";
				resultsDetail += "總共獲得了 ";
				
				int temp_exp;
				if(gameStage.win)
					temp_exp = gameStage.Killnum * gameStage.ExpPerMonster + gameStage.ExpBoss;
				else
					temp_exp = gameStage.Killnum * gameStage.ExpPerMonster;
				resultsDetail += temp_exp.ToString() + " 經驗值\n\n";
				
				resultsDetail += (gameStage.win?"因為獲得了勝利，心情變好了":"但因為被打敗而心情變差了");


				detail.text = resultsDetail;
			}

			//get Pet
			if(gameStage.win && Random.Range(0,100)>(0-getPetPersentage))
			{
				Debug.Log(0-getPetPersentage);
				newPetPanel.GetComponent<NewPetPanel>().NewPetNaming(gameStage.dropPetKind);
			}
		}
		public void SetToPlayer()
		{
			for(int i=0;i<FoodController.foodsNumber;++i)
			{
				PlayerData.instance.AddFood(i, gameStage.foodsCounter[i]);
			}
			if(gameStage.win)
			{
				pet.AddExp(gameStage.Killnum * gameStage.ExpPerMonster + gameStage.ExpBoss);
				pet.AddMood(MoodChanged);
			}
			else
			{
				pet.AddExp(gameStage.Killnum * gameStage.ExpPerMonster);
				pet.AddMood(-MoodChanged);
			}
		}

		public void BackToHome()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("InteractiveScene");
		}
	}
}