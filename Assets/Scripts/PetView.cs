using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace SpiritPetMaster
{
    public class PetView : MonoBehaviour
    {
        public Image PetImage;

        [Header("Pet Data")]
        public Pet PetData;

        void Start()
        {
            if(PetData.PetSpriteName != null)
            {
                var sprite = Resources.LoadAll(PetData.PetSpriteName, typeof(Sprite));
                if (sprite.Length > 0)
                {
                    PetImage.sprite = (Sprite)sprite[0];
                }
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
