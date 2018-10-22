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
        public Slider PetMood;

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
                PetName.text = CurrentPet.Name;
                PetMood.value = CurrentPet.Mood;
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


