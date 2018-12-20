using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpiritPetMaster
{
	public class LevelUpPanel : MonoBehaviour {

		public PetView pet;
		public Image petImage;
		public Text LevelValue;
        public Slider Exp;
		public Text ExpValue;

		void Start () {
			
		}

		void Update () {

		}

		void OnEnable(){
			OpenPanel(PetViewController.instance.CurrentFocusPet());
		}

		void OpenPanel(PetView _pet){
			if(_pet!=null)
			{
				pet = _pet;
				LevelValue.text = pet.Level.ToString();
				Exp.maxValue = pet.maxExp;
				Exp.value = pet.Exp;
				ExpValue.text = pet.Exp.ToString() + " / " + pet.maxExp.ToString();

				gameObject.SetActive(true);
			}
		}
	}
}