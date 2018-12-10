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

        [Header("Talking Box")]
        public GameObject TakingBox;
        public Text PetTalking;

        [Header("Pet Data")]
        public Pet CurrentPet;

        #region private

        string[] pet_taking_contents;

        #endregion

        public void AssignPet(Pet _pet)
        {
            CurrentPet = _pet;

            TakingBox.SetActive(false);

            UpdateInfo();
        }

        public void UpdateInfo()
        {
            if(CurrentPet != null)
            {
                PetName.text = CurrentPet.Name;
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
                Debug.LogFormat("not found current pet");
            }
            
        }

        public void ChangeTakingContent()
        {
            /* Showing a pet taking content */
            string[] pet_talking_contents = CurrentPet.PetTalkingContents;
            if ((pet_talking_contents != null) && (pet_talking_contents.Length > 0))
            {
                TakingBox.SetActive(true);
                PetTalking.text = pet_talking_contents[Random.Range(0, pet_talking_contents.Length)];
            }
            else
            {
                TakingBox.SetActive(false);
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

        void LateUpdate()
        {
            UpdateInfo();
        }
    }
}


