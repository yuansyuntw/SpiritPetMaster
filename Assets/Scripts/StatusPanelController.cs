using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpiritPetMaster
{
    public class StatusPanelController : MonoBehaviour {

            public PetView pet;
            public Image petImage;
            public Text PointsValue;
            public Text MaxHPValue;
            public Text HPRecoverValue;
            public Text MaxMPValue;
            public Text MPRecoverValue;
            public Text AttackValue;
            public Text DefenceValue;
            public Text FireAttackValue;
            public Text WaterAttackValue;
            public Text WindAttackValue;

            private int points = 0;

        void Start () {
            MaxHPValue.text = "0";
            HPRecoverValue.text = "0";
            MaxMPValue.text = "0";
            MPRecoverValue.text = "0";
            AttackValue.text = "0";
            DefenceValue.text = "0";
            FireAttackValue.text = "0";
            WaterAttackValue.text = "0";
            WindAttackValue.text = "0";
            PointsValue.text = "0";
        }

        void OnEnable() {
            OpenStatus(PetViewController.instance.CurrentFocusPet());
        }
        
        void FixedUpdate () {
            if(pet != null){
                MaxHPValue.text = pet.MaxHP.ToString();
                HPRecoverValue.text = pet.HPRecover.ToString();
                MaxMPValue.text = pet.MaxMP.ToString();
                MPRecoverValue.text = pet.MPRecover.ToString();
                AttackValue.text = pet.Attack.ToString();
                DefenceValue.text = pet.Defence.ToString();
                FireAttackValue.text = pet.PetfireAttack.ToString();
                WaterAttackValue.text = pet.PetwaterAttack.ToString();
                WindAttackValue.text = pet.PetwindAttack.ToString();
                PointsValue.text = points.ToString();
            }
        }

        void OpenStatus(PetView pet_){
            pet = pet_;
            updateImage();
        }

        void updateImage(){
            if (pet.PetSpriteName != "")
            {
                var img = Resources.Load(pet.PetSpriteName, typeof(Image));
                if (img != null)
                {
                    petImage = (Image)img;
                }
            }
        }

        void AddPoints(){

        }
    }
}