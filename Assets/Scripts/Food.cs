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


        string PetTag = "PetView";
        AudioSource sound;



        // Use this for initialization
        void Start()
        {
            sound = GetComponent<AudioSource>();
            //GetComponent<SpriteRenderer>().size = FoodController.Instantiate.FoodSize;
        }




        // Update is called once per frame
        void Update()
        {

        }




        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.CompareTag(PetTag))
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
                pet.Attack += EffectAttack;
                pet.Defence += EffectDefence;

                if(sound != null)   sound.Play();
            }
            Destroy(gameObject);
        }
    }
}


