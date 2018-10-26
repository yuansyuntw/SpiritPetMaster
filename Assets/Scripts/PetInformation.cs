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
        public Text PetTalking;
        public Slider PetMood;
        public Slider PetHunger;

        [Header("Pet Data")]
        public Pet CurrentPet;

        #region private

        string[] pet_taking_contents;

        #endregion

        public void AssignPet(Pet _pet)
        {
            CurrentPet = _pet;

            UpdateInfo();
        }

        public void UpdateInfo()
        {
            if(CurrentPet != null)
            {
<<<<<<< HEAD
                PetName.text = CurrentPet.PetName;
                PetLevel.text = "LV." + CurrentPet.Level;
=======
                // PetName.text = CurrentPet.Name;
>>>>>>> animation
                PetMood.value = CurrentPet.Mood;
                PetHunger.value = CurrentPet.Hunger;

                /* Showing a pet taking content */
                string[] pet_taking_contents = CurrentPet.PetTakingContents;
                if ((pet_taking_contents != null) && (pet_taking_contents.Length>0))
                {
                    PetTalking.text = pet_taking_contents[Random.Range(0, pet_taking_contents.Length)];
                }
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


