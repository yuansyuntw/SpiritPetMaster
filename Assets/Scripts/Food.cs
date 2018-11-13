using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class Food : MonoBehaviour
    {
        public Sprite FoodSprite;
        public int AddHunger;

        string PetTag = "PetView";
        AudioSource sound;



        // Use this for initialization
        void Start()
        {
            sound = GetComponent<AudioSource>();
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

                if(sound != null)   sound.Play();
            }
            Destroy(gameObject);
        }
    }
}


