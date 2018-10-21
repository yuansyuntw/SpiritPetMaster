using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;


namespace SpiritPetMaster
{
    public class SwitchPetView : MonoBehaviour
    {
        [Header("Pet View Object")]
        public Transform PetView;

        [Header("Pet View Entering Points")]
        public Transform RightEnterPoint;
        public Transform LeftEnterPoint;
        public float EnteringTime = 3f;

        [Header("Pet Information UI")]
        public Text PetName;
        public Slider PetMood;


        #region GOBAL VARIABLE

        const int LEFT = 0;
        const int RIGHT = 1;

        #endregion

        #region private
        Pet current_pet_data;
        #endregion

        public void NextPetView()
        {
            Pet new_pet_data = PlayerData.instance.PetViewGetNextPetData();
            if(new_pet_data != current_pet_data)
            {
                current_pet_data = new_pet_data;
                UpdatePetView(RIGHT);
            }
        }

        public void LeftPetView()
        {
            Pet new_pet_data = PlayerData.instance.PetViewGetLastPetData();
            if (new_pet_data != current_pet_data)
            {
                current_pet_data = new_pet_data;
                UpdatePetView(LEFT);
            }
        }

        void UpdatePetView(int _update_effect)
        {
            /*
            PetName.text = current_pet_data.Name;
            PetMood.value = current_pet_data.Mood;
            */

            if(_update_effect == RIGHT)
            {
                PetView.position = RightEnterPoint.position;
                PetView.DOMove(Vector3.zero, EnteringTime);
            }
            else
            {
                PetView.position = LeftEnterPoint.position;
                PetView.DOMove(Vector3.zero, EnteringTime);
            }

        }
    }
}

