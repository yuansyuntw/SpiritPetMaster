using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class Food : MonoBehaviour
    {
        public Sprite FoodSprite;
        public int index;
        public int AddHunger;

        public float EffectMaxHP = 0;
        public float EffectHPRecover = 0;
        public float EffectMaxMP = 0;
        public float EffectMPRecover = 0;
        public float EffectAttack = 0;
        public float EffectDefence = 0;

        public string Description = "";

        GameObject StatusUpText;
        private float spawn_time = 0;
        public float life_time = 5f;

        string PetTag = "PetView";
        AudioSource sound;



        // Use this for initialization
        void Start()
        {
            sound = GetComponent<AudioSource>();
            //GetComponent<SpriteRenderer>().size = FoodController.Instantiate.FoodSize;
            spawn_time = Time.time;
        }




        // Update is called once per frame
        void LateUpdate()
        {
            if(Time.time-spawn_time > life_time)
                Destroy(gameObject);
        }




        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag(PetTag) && collider.gameObject.GetComponent<PetView>().ID == PlayerData.instance.GetPlayerFocusPetId())
            {
                PetView pet = collider.GetComponent<PetView>();

                pet.IncreateHunger(AddHunger);
                pet.EatFood(this);
                

                if(sound != null)   sound.Play();
                
                Destroy(gameObject);
            }
        }

        
        public void AddStatus(ref float _status, float _add, string statusType="能力值")
        {
            if(_status+_add > Pet.UpgradeMax)
            {
                _add = Pet.UpgradeMax - _status;
            }
            else if(_status+_add < Pet.UpgradeMin)
            {
                _add = Pet.UpgradeMin - _status;
            }

            if(_add == 0)
            {
                //show text
                //StatusUpText;
            }
            else
            {
                _status += _add;
                //show text
                //StatusUpText;
            }
        }
    }
}


