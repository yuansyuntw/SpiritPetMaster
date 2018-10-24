using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;


namespace SpiritPetMaster
{
    public class SwitchPetView : MonoBehaviour
    {
        [Header("Prefabs")]
        public GameObject PetView;

        [Header("Pet View Translating")]
        public int ViewWidth = 1000;
        public float TranslatingTime = 3f;
        public Transform PetViewParent;

        [Header("Pet Information UI")]
        public PetInformation PetInfoController;



        #region GOBAL VARIABLE

        const int LEFT = 0;
        const int RIGHT = 1;

        #endregion




        #region private

        Pet current_pet_data;
        List<Pet> current_pets_data = new List<Pet>();
        List<GameObject> pet_views = new List<GameObject>();
        int current_view_index = 0;
        UnityAction PlayerDataUpdateEvetns;

        #endregion



        #region public api

        public void NextPetView()
        {
            if(current_view_index + 1 < current_pets_data.Count)
            {
                current_view_index++;
                FocusPet(current_view_index);
            }
        }



        public void LeftPetView()
        {
            if(current_view_index - 1 >= 0)
            {
                current_view_index--;
                FocusPet(current_view_index);
            }
        }



        public void FocusPet(int _view_index)
        {
            /* Move pet view */
            PetViewParent.DOMoveX(-_view_index*ViewWidth, TranslatingTime);

            /* Update pet information controller */
            Debug.LogFormat("now view index: {0}/{1}", _view_index, current_pets_data.Count);
            PetInfoController.AssignPet(current_pets_data[_view_index]);
        }



        public void UpdatePetView()
        {
            List<Pet> _player_pets = PlayerData.instance.PetViewGetPets();
            current_pets_data = _player_pets;
            current_view_index = 0;

            /* Destroy a old pet view*/
            if (pet_views.Count > 0)
            {
                for (int i = 0; i < pet_views.Count; i++)
                {
                    Destroy(pet_views[i]);
                }
            }

            /* Add a new pet view */
            Vector3 original = Vector3.zero;
            for(int i = 0; i < current_pets_data.Count; i++)
            {
                Vector3 new_view_pos = original + new Vector3(i*ViewWidth, 0, 0);
                //Debug.LogFormat("view x' pos[{0}] = {1}", i, new_view_pos.x);
                GameObject _new_pet_view = Instantiate(PetView, new_view_pos, Quaternion.identity, PetViewParent);

                PetView _pet_view = _new_pet_view.GetComponent<PetView>();
                _pet_view.PetData = current_pets_data[i];

                pet_views.Add(_new_pet_view);
            }

            /* Reset the pet view postition */
            FocusPet(0);

            Debug.LogFormat("now pet count: {0}", current_pets_data.Count);
        }

        #endregion

        void Start()
        {
            if(PetViewParent == null)
            {
                PetViewParent = Instantiate(new GameObject("PetViewParent"), transform).GetComponent<Transform>();
            }

            /* Registe the update pet view events to the plater data */
            PlayerDataUpdateEvetns += UpdatePetView;
            PlayerData.instance.RegistePlayerDataUpdateEvents(PlayerDataUpdateEvetns);

            UpdatePetView();
        }

        void OnDestroy()
        {
            /* Remove the update pet view events from the player data */
            PlayerData.instance.RemovePlayerDataUpdateEvents(PlayerDataUpdateEvetns);
        }
    }
}

