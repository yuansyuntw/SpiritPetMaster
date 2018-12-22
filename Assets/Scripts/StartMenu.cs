using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpiritPetMaster{
	public class StartMenu : MonoBehaviour {

		public bool nameUsed = false;
		public GameObject sameNameWarning;
		public GameObject petsContainer;
		public GameObject startMenu;
		public NewPetPanel newPetPanel;

		public void isUsed (Text _name){
			PlayerData.instance.PlayerName = _name.text;
			Debug.Log(PlayerData.instance.PlayerName);
			string[] _petids = PlayerData.instance.GetPetsId();
			if(_petids==null || _petids.Length==0)
				nameUsed = false;
			else
				nameUsed = true;
		}

		public void toNameWarning(){
			if(nameUsed){
				sameNameWarning.SetActive(true);
			}
			else{
				reloadUser();
				startMenu.SetActive(false);
				newPetPanel.NewPetNaming("Slime");
			}
		}

		public void AddNewPet(string _petName){
			PlayerData.instance.AddNewPet(_petName, newPetPanel.GetNewPetName());
		}
		public void rebuildUser(string _name){
			PlayerData.instance.PlayerName = _name;
			rebuildUser();
		}
		public void rebuildUser(){
			PlayerData.instance.ClearPlayerData();
			reloadUser();
			newPetPanel.NewPetNaming("Slime");
		}
		public void reloadUser(string _name){
			PlayerData.instance.PlayerName = _name;
			reloadUser();
		}
		public void reloadUser(){
			petsContainer.GetComponent<PetViewController>().ClearPetsInScene();
			petsContainer.GetComponent<PetViewController>().ReloadPets();
		}

	}
}