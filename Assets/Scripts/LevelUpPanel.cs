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
		public GameObject ArrowMaxHP;
		public GameObject ArrowHPRecover;
		public GameObject ArrowMaxMP;
		public GameObject ArrowMPRecover;
		public GameObject ArrowAttack;
		public GameObject ArrowDefence;
		const float minScale = 0.2f, maxScale = 1.2f;

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
				
				float temp = (pet.UpgradeMaxHP - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				float scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowMaxHP.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				temp = (pet.UpgradeHPRecover - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowHPRecover.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				temp = (pet.UpgradeMaxMP - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowMaxMP.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				temp = (pet.UpgradeMPRecover - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowMPRecover.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				temp = (pet.UpgradeAttack - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowAttack.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				temp = (pet.UpgradeDefence - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowDefence.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				gameObject.SetActive(true);
			}
		}
		void UpdatePanel(){
			if(pet!=null)
			{
				LevelValue.text = pet.Level.ToString();
				Exp.maxValue = pet.maxExp;
				Exp.value = pet.Exp;
				ExpValue.text = pet.Exp.ToString() + " / " + pet.maxExp.ToString();
				
				float temp = (pet.UpgradeMaxHP - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				float scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowMaxHP.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				temp = (pet.UpgradeHPRecover - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowHPRecover.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				temp = (pet.UpgradeMaxMP - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowMaxMP.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				temp = (pet.UpgradeMPRecover - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowMPRecover.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				temp = (pet.UpgradeAttack - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowAttack.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);

				temp = (pet.UpgradeDefence - Pet.UpgradeMin)/(Pet.UpgradeMax - Pet.UpgradeMin);
				scaleValue = minScale + (maxScale-minScale)*temp;
				ArrowDefence.transform.localScale = new Vector3(scaleValue,scaleValue,scaleValue);
			}
		}

		public void PetLevelUP()
		{
			pet.LevelUp();
			UpdatePanel();
		}
	}
}