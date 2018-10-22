using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace SpiritPetMaster
{
    public class PetView : MonoBehaviour
    {
        public Image PetImage;

        public Pet PetData;

        void Start()
        {
            if(PetData.PetSprite != null)
            {
                PetImage.sprite = PetData.PetSprite;
            }
        }

        public void OnMouseDown()
        {
            PetData.Mood++;
            PetInformation.instance.UpdateInfo();
            Debug.LogFormat("Click");
        }

        public void OnMouseEnter()
        {
            Debug.LogFormat("Enter");
        }

    }
}
