using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpiritPetMaster
{
    public class StatusPanelController : MonoBehaviour {

            public PetView pet;
            public Image petImage;
            public Text Name;
            public Text LevelValue;
            public Slider Mood;
            public Slider Hunger;
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


            public Text MaxHPUpgradeValue;
            public Text HPRecoverUpgradeValue;
            public Text MaxMPUpgradeValue;
            public Text MPRecoverUpgradeValue;
            public Text AttackUpgradeValue;
            public Text DefenceUpgradeValue;

            private const int UpgradeLimit = 100;
            private int UpgradeMaxHP;
            private int UpgradeHPRecover;
            private int UpgradeMaxMP;
            private int UpgradeMPRecover;
            private int UpgradeAttack;
            private int UpgradeDefence;


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
            
            MaxHPUpgradeValue.text = "0";
            HPRecoverUpgradeValue.text = "0";
            MaxMPUpgradeValue.text = "0";
            MPRecoverUpgradeValue.text = "0";
            AttackUpgradeValue.text = "0";
            DefenceUpgradeValue.text = "0";
        }

        void OnEnable() {
            OpenStatus(PetViewController.instance.CurrentFocusPet());
        }
        
        void LateUpdate () {
            if(pet != null){
                Mood.value = pet.Mood;
                Hunger.value = pet.Hunger;

                Name.text = pet.Name;
                LevelValue.text = pet.Level.ToString();
                
                FireAttackValue.text = pet.PetfireAttack.ToString();
                WaterAttackValue.text = pet.PetwaterAttack.ToString();
                WindAttackValue.text = pet.PetwindAttack.ToString();

                MaxHPValue.text = (pet.MaxHP+UpgradeMaxHP*10).ToString();
                HPRecoverValue.text = (pet.HPRecover+UpgradeHPRecover*0.005f).ToString();
                MaxMPValue.text = (pet.MaxMP+UpgradeMaxMP*10).ToString();
                MPRecoverValue.text = (pet.MPRecover+UpgradeMPRecover*0.005f).ToString();
                AttackValue.text = (pet.PetAttack+UpgradeAttack).ToString();
                DefenceValue.text = (pet.PetDefence+UpgradeDefence).ToString();
                
                PointsValue.text = points.ToString();
                
                MaxHPUpgradeValue.text = UpgradeMaxHP.ToString();
                HPRecoverUpgradeValue.text = UpgradeHPRecover.ToString();
                MaxMPUpgradeValue.text = UpgradeMaxMP.ToString();
                MPRecoverUpgradeValue.text = UpgradeMPRecover.ToString();
                AttackUpgradeValue.text = UpgradeAttack.ToString();
                DefenceUpgradeValue.text = UpgradeDefence.ToString();
            }
        }

        void OpenStatus(PetView pet_){
            pet = pet_;
            points = pet.Points;
            updateImage();
        }

        void updateImage(){
            if (pet.PetSpriteName != "")
            {
                var img = Resources.Load(pet.PetSpriteName, typeof(Sprite));
                if (img != null)
                {
                    petImage.sprite = (Sprite)img;
                }
            }
        }

        public void AddPoints(string valueName){
            if(points>0)
            {
                switch(valueName)
                {
                    case "MaxHP":
                        points--;
                        UpgradeMaxHP++;
                    break;
                    case "HPRecover":
                        points--;
                        UpgradeHPRecover++;
                    break;
                    case "MaxMP":
                        points--;
                        UpgradeMaxMP++;
                    break;
                    case "MPRecover":
                        points--;
                        UpgradeMPRecover++;
                    break;
                    case "Attack":
                        points--;
                        UpgradeAttack++;
                    break;
                    case "Defence":
                        points--;
                        UpgradeDefence++;
                    break;
                    default:
                    break;
                }
            }
        }
        public void MinusPoints(string valueName){
            switch(valueName)
            {
                case "MaxHP":
                    if(UpgradeMaxHP>0){
                        UpgradeMaxHP--;
                        points++;
                    }
                break;
                case "HPRecover":
                    if(UpgradeHPRecover>0){
                        UpgradeHPRecover--;
                        points++;
                    }
                break;
                case "MaxMP":
                    if(UpgradeMaxMP>0){
                        UpgradeMaxMP--;
                        points++;
                    }
                break;
                case "MPRecover":
                    if(UpgradeMPRecover>0){
                        UpgradeMPRecover--;
                        points++;
                    }
                break;
                case "Attack":
                    if(UpgradeAttack>0)
                    {
                        UpgradeAttack--;
                        points++;
                    }
                break;
                case "Defence":
                    if(UpgradeDefence>0){
                        UpgradeDefence--;
                        points++;
                    }
                break;
                default:
                break;
            }
        }
        

        public void ResetPoints(){
            points += UpgradeMaxHP;
            points += UpgradeHPRecover;
            points += UpgradeMaxMP;
            points += UpgradeMPRecover;
            points += UpgradeAttack;
            points += UpgradeDefence;
            UpgradeMaxHP = 0;
            UpgradeHPRecover = 0;
            UpgradeMaxMP = 0;
            UpgradeMPRecover = 0;
            UpgradeAttack = 0;
            UpgradeDefence = 0;
        }

        public void SetUpgrades(){
            pet.Points = points;
            pet.MaxHP += UpgradeMaxHP*10;
            pet.HPRecover += UpgradeHPRecover*0.005f;
            pet.MaxMP += UpgradeMaxMP*10;
            pet.MPRecover += UpgradeMPRecover*0.005f;
            pet.PetAttack += UpgradeAttack;
            pet.PetDefence += UpgradeDefence;
            UpgradeMaxHP = 0;
            UpgradeHPRecover = 0;
            UpgradeMaxMP = 0;
            UpgradeMPRecover = 0;
            UpgradeAttack = 0;
            UpgradeDefence = 0;
        }
    }
}