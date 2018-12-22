using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpiritPetMaster
{
    public class NewPetPanel : MonoBehaviour
    {
        public string PetKind = "";
        public Image PetImage;
        public Text PetNameInput;
        public GameObject PetViewPrefab;

        public void SetPetImage()
        {
            var sprite = Resources.Load(PetKind, typeof(Sprite));
            if (sprite != null)
            {
                PetImage.sprite = (Sprite)sprite;
            }
        }

        public string GetNewPetName()
        {
            return PetNameInput.text;
        }

        public void NewPetNaming(string _petkind)
        {
            PetKind = _petkind;
            NewPetNaming();
        }
        public void NewPetNaming()
        {
            SetPetImage();
            gameObject.SetActive(true);
        }

        public void AddNewPet()
        {
            PetViewController.instance.NewPetView(PetKind, PetNameInput.text);
        }
        public void AddNewPetForStage()
        {
            int id;
            do
            {
                id = Random.Range(0, PetViewController.PET_ID_RANGER);
            } while (!CheckID(id));
            GameObject _new_pet_view = Instantiate(PetViewPrefab, Vector3.zero, Quaternion.identity, transform);
            _new_pet_view.SetActive(false);
            PetView _pet_view = _new_pet_view.GetComponent<PetView>();
            _pet_view.NewPet(id, PetKind, GetNewPetName());

            PlayerData.instance.SavePlayerData();
        }

    }
}


