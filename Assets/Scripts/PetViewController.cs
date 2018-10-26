using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

namespace SpiritPetMaster
{
    public class PetViewController : MonoBehaviour
    {

        static public PetViewController instance;

        [Header("Pet View Size")]
        public int ContainerWidth = 1000;
        public int ContainerHeight = 500;

        [Header("Pet View Prefabs")]
        public GameObject PetViewPrefab;

        [Header("Pet View Translating")]
        public int ViewWidth = 50;
        public float ViewMovingSpeed = 2.5f;

        [Header("Pet Information UI")]
        public GameObject InformationUI;
        public PetInformation PetInfoController;



        #region GOBAL VARIABLE

        const int STOP = 0;
        const int LEFT = -1;
        const int RIGHT = 1;

        #endregion




        #region private

        Transform camera_transform;
        int camera_moving_direction;
        PetView current_focus_pet;

        Pet current_pet_data;
        List<Pet> current_pets_data = new List<Pet>();
        List<PetView> pet_views = new List<PetView>();
        int current_view_index = 0;
        UnityAction player_update_date_events;

        float PET_VIEWS_POS_RANGE = 0.9f; 

        #endregion



        #region public api

        public void RightPetView()
        {
            if(current_view_index + 1 < current_pets_data.Count)
            {
                current_view_index++;
            }

            /* Closing the pet information */
            FreePetView();

            /* Moving the pet view */
            FocusPetView(pet_views[current_view_index]);
        }



        public void LeftPetView()
        {
            if(current_view_index - 1 >= 0)
            {
                current_view_index--;
            }

            /* Closing the pet information */
            FreePetView();

            /* Moving the pet view */
            FocusPetView(pet_views[current_view_index]);
        }



        public void FocusPetView(PetView _focus_pet)
        {
            if(current_focus_pet == null)
            {
                /* Moveing camera */
                current_focus_pet = _focus_pet;
                Vector3 pos = current_focus_pet.transform.position;
                if (pos.y < 0) pos.y = 0;
                camera_transform.DOMove(pos, 1.5f);

                /* Showing the pet information */
                InformationUI.SetActive(true);
                Pet pet = current_focus_pet.PetData;
                PetInformation.instance.AssignPet(pet);
            }
        }



        public void FreePetView()
        {
            /* Closing the pet information */
            InformationUI.SetActive(false);

            current_focus_pet = null;
        }



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
                    Destroy(pet_views[i].gameObject);
                }
            }
            pet_views.Clear();

            /* Add a new pet view */
            if (PetViewPrefab != null)
            {
                Vector3 original = Vector3.zero;
                for (int i = 0; i < current_pets_data.Count; i++)
                {
                    Vector3 new_view_pos = original + new Vector3(Random.Range(-ContainerWidth * PET_VIEWS_POS_RANGE, ContainerWidth * PET_VIEWS_POS_RANGE), Random.Range(0, ContainerHeight * PET_VIEWS_POS_RANGE), 0);

                    GameObject _new_pet_view = Instantiate(PetViewPrefab, new_view_pos, Quaternion.identity, transform);

                    PetView _pet_view = _new_pet_view.GetComponent<PetView>();
                    _pet_view.PetData = current_pets_data[i];

                    pet_views.Add(_pet_view);

                }
            }

            Debug.LogFormat("now pet count: {0}", current_pets_data.Count);
        }

        #endregion


        #region life cycle

        void Awake()
        {
            if (PetViewController.instance == null)
            {
                PetViewController.instance = this;
            }
            else
            {
                if (PetViewController.instance != this)
                {
                    Destroy(this);
                }
            }
        }



        void Start()
        {
            /* Get the transform of the main camera */
            camera_transform = Camera.main.transform;

            /* Registe the update pet view events to the plater data */
            player_update_date_events += UpdatePetView;
            PlayerData.instance.RegistePlayerDataUpdateEvents(player_update_date_events);

            UpdatePetView();
        }



        void Update()
        {

            if (camera_moving_direction == LEFT)
            {
                /* Moving camera for left */
                if (camera_transform.position.x > -1 * ViewWidth * 0.8f)
                {
                    camera_transform.Translate(-1 * camera_transform.right * ViewMovingSpeed * Time.deltaTime);
                }
            }
            else if (camera_moving_direction == RIGHT)
            {
                /* Moving camera for right */
                if (camera_transform.position.x < ViewWidth * 0.8f)
                {
                    camera_transform.Translate(camera_transform.right * ViewMovingSpeed * Time.deltaTime);
                }
            }
        }

        void OnDisable()
        {
            /* Remove the update pet view events from the player data */
            PlayerData.instance.RemovePlayerDataUpdateEvents(player_update_date_events);
        }

        #endregion
    }
}
