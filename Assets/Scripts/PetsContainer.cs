using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

namespace SpiritPetMaster
{
    public class PetsContainer : MonoBehaviour
    {

        #region public 

        [Header("Conatiner")]
        public int ContainerWidth = 1000;
        public int ContainerHeight = 500;

        [Header("Pet View Prefabs")]
        public GameObject PetViewPrefab;

        #endregion

        #region private

        Pet current_pet_data;
        List<Pet> current_pets_data = new List<Pet>();
        List<GameObject> pet_views = new List<GameObject>();
        int current_view_index = 0;
        UnityAction player_update_date_events;

        #endregion

        public void UpdatePetView()
        {
            List<Pet> _player_pets = PlayerData.instance.PetViewGetPets();
            current_pets_data = _player_pets;
            current_view_index = 0;

            /* Destroy a old pet view */
            if (pet_views.Count > 0)
            {
                for (int i = 0; i < pet_views.Count; i++)
                {
                    Destroy(pet_views[i]);
                }
            }

            /* Add a new pet view */
            if(PetViewPrefab != null)
            {
                Vector3 original = Vector3.zero;
                for (int i = 0; i < current_pets_data.Count; i++)
                {
                    Vector3 new_view_pos = original + new Vector3(Random.Range(-ContainerWidth, ContainerWidth), Random.Range(0, ContainerHeight), 0);

                    GameObject _new_pet_view = Instantiate(PetViewPrefab, new_view_pos, Quaternion.identity, transform);

                    PetView _pet_view = _new_pet_view.GetComponent<PetView>();
                    _pet_view.PetData = current_pets_data[i];

                    pet_views.Add(_new_pet_view);

                }
            }

            Debug.LogFormat("now pet count: {0}", current_pets_data.Count);
        }

        void Start()
        {
            /* Registe the update pet view events to the plater data */
            player_update_date_events += UpdatePetView;
            PlayerData.instance.RegistePlayerDataUpdateEvents(player_update_date_events);

            UpdatePetView();
        }

        void OnDisable()
        {
            /* Remove the update pet view events from the player data */
            PlayerData.instance.RemovePlayerDataUpdateEvents(player_update_date_events);
        }
    }
}

