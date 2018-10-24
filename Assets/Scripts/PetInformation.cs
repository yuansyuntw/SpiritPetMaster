using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace SpiritPetMaster
{
    public class PetInformation : MonoBehaviour
    {
        static public PetInformation instance;

        [Header("UI")]
        public Text PetName;
        public Text PetLevel;
        public Slider PetMood;
        public Slider PetHunger;

        public Pet CurrentPet;

        public void AssignPet(Pet _pet)
        {
            CurrentPet = _pet;
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            if(CurrentPet != null)
            {
                PetName.text = CurrentPet.PetName;
                PetLevel.text = "LV." + CurrentPet.Level;
                PetMood.value = CurrentPet.Mood;
                PetHunger.value = CurrentPet.Hunger;
            }
            else
            {
                PetName.text = "";
                PetLevel.text = "";
                PetMood.value = 0;
                PetHunger.value = 0;
            }
        }

        void Awake()
        {
            if(PetInformation.instance == null)
            {
                PetInformation.instance = this;
            }
            else
            {
                if(PetInformation.instance != this)
                {
                    Destroy(this);
                }
            }
        }

    }
}


