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
        public int EffectAttack = 0;
        public int EffectDefence = 0;

        public string Description = "";

        private float spawn_time;
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
                
                /*
                pet.UpgradeMaxHP += EffectMaxHP;
                pet.UpgradeHPRecover += EffectHPRecover;
                pet.UpgradeMaxMP += EffectMaxMP;
                pet.UpgradeMPRecover += EffectMPRecover;
                pet.UpgradeAttack += EffectAttack;
                pet.UpgradeDefence += EffectDefence;
                */
                pet.MaxHP += EffectMaxHP;
                pet.HPRecover += EffectHPRecover;
                pet.MaxMP += EffectMaxMP;
                pet.MPRecover += EffectMPRecover;
                pet.PetAttack += EffectAttack;
                pet.PetDefence += EffectDefence;

                if(sound != null)   sound.Play();
                
                Destroy(gameObject);
            }
        }
    }
}


