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
        List<PetView> pets_view_data = new List<PetView>();
        int current_view_index = 0;
        UnityAction player_update_date_events;

        float PET_VIEWS_POS_RANGE = 0.9f;
        int PET_ID_RANGER = 1000;

        #endregion



        #region public api

        public void RightPetView()
        {
            if(current_view_index + 1 < pets_view_data.Count)
            {
                current_view_index++;
            }

            if((current_view_index < pets_view_data.Count) && (current_view_index >= 0))
            {
                /* Closing the pet information */
                FreePetView();

                /* Moving the pet view */
                FocusPetView(pets_view_data[current_view_index]);
            }
        }



        public void LeftPetView()
        {
            if(current_view_index - 1 >= 0)
            {
                current_view_index--;
            }

            if ((current_view_index < pets_view_data.Count) && (current_view_index >= 0))
            {
                /* Closing the pet information */
                FreePetView();

                /* Moving the pet view */
                FocusPetView(pets_view_data[current_view_index]);
            }
        }


        
        public void TouchPetView(PetView _touched_pet)
        {
            /* Player want to focus a pet */
            if(current_focus_pet == null)
            {
                FocusPetView(_touched_pet);
            }

            /* Player is interactive with the touched pet */
            if(current_focus_pet == _touched_pet)
            {
                _touched_pet.IncreateMood();
            }

            /* Player is clicking other pet*/
            if(current_focus_pet != _touched_pet)
            {
                FreePetView();
            }
        }



        public void FocusPetView(PetView _focus_pet)
        {
            /* Moveing camera */
            current_focus_pet = _focus_pet;
            Vector3 pos = current_focus_pet.transform.position;
            if (pos.y < 0) pos.y = 0;
            camera_transform.DOMove(pos, 1.5f);

            /* Showing the pet information */
            InformationUI.SetActive(true);
            Pet pet = current_focus_pet;
            PetInformation.instance.AssignPet(pet);
            //Debug.LogFormat("{0}: {1}", current_focus_pet, pet);
        }



        public void FreePetView()
        {
            /* Closing the pet information */
            InformationUI.SetActive(false);

            current_focus_pet = null;
        }



        public void NewPetView(int _kind, string _name)
        {
            /* Random a id */
            int id;
            do
            {
                id = Random.Range(0, PET_ID_RANGER);
            } while (!CheckID(id));

            /* A insatnced position in background*/
            Vector3 new_view_pos = Vector3.zero + new Vector3(Random.Range(-ContainerWidth * PET_VIEWS_POS_RANGE, ContainerWidth * PET_VIEWS_POS_RANGE), Random.Range(0, ContainerHeight * PET_VIEWS_POS_RANGE), 0);

            /* Instantiate PetView prefab and load it data by it's id */
            GameObject _new_pet_view = Instantiate(PetViewPrefab, new_view_pos, Quaternion.identity, transform);
            PetView _pet_view = _new_pet_view.GetComponent<PetView>();
            _pet_view.NewPet(id, _kind, _name);

            pets_view_data.Add(_pet_view);

            PlayerData.instance.SavePlayerData();
        }



        public void UpdatePetView()
        {
            string[] _petids = PlayerData.instance.GetPetsId();
            current_view_index = 0;

            /* Destroy a old pet view */
            if (pets_view_data.Count > 0)
            {
                for (int i = 0; i < pets_view_data.Count; i++)
                {
                    Destroy(pets_view_data[i].gameObject);
                }
            }
            pets_view_data.Clear();

            /* Add a new pet view */
            if ((_petids != null) && (PetViewPrefab != null))
            {
                for (int i = 0; i < _petids.Length; i++)
                {
                    int id;
                    int.TryParse(_petids[i], out id);

                    /* A insatnced position in background*/
                    Vector3 new_view_pos = Vector3.zero + new Vector3(Random.Range(-ContainerWidth * PET_VIEWS_POS_RANGE, ContainerWidth * PET_VIEWS_POS_RANGE), Random.Range(0, ContainerHeight * PET_VIEWS_POS_RANGE), 0);

                    /* Instantiate PetView prefab and load it data by it's id */
                    GameObject _new_pet_view = Instantiate(PetViewPrefab, new_view_pos, Quaternion.identity, transform);
                    PetView _pet_view = _new_pet_view.GetComponent<PetView>();                
                    _pet_view.LoadPet(id);

                    Animator _pet_animator = _new_pet_view.GetComponent<Animator>();


                    pets_view_data.Add(_pet_view);
                }
            }

            /* Set back the PlayerDaata, waitting next time to save */
            PlayerData.instance.SetPetViews(pets_view_data);

            Debug.LogFormat("now pet count: {0}", pets_view_data.Count);
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

        }

        #endregion



        bool CheckID(int _id)
        {
            bool result = true;

            for (int i = 0; i < pets_view_data.Count; i++)
            {
                if (_id == pets_view_data[i].ID)
                {
                    result = false;
                }
            }

            return result;
        }
        
    }
}
