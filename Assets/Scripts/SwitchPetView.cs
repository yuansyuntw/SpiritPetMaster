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
        static public SwitchPetView instance;

        [Header("Pet View Translating")]
        public int ViewWidth = 50;
        public float ViewMovingSpeed = 2.5f;

        [Header("Pet Information UI")]
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

        #endregion



        #region public api

        public void RightPetView()
        {
            camera_moving_direction = RIGHT;
        }



        public void LeftPetView()
        {
            camera_moving_direction = LEFT;
        }



        public void StopPetView()
        {
            camera_moving_direction = STOP;
        }



        public void FocusPet(PetView _focus_pet)
        {
            if(current_focus_pet != _focus_pet)
            {
                current_focus_pet = _focus_pet;
                Vector3 pos = current_focus_pet.transform.position;
                pos.y = 0;
                camera_transform.DOMove(pos, 1.5f);
            }
        }



        public void FreePet()
        {
            current_focus_pet = null;
        }

        #endregion


        #region life cycle

        void Awake()
        {
            if(SwitchPetView.instance == null)
            {
                SwitchPetView.instance = this;
            }
            else
            {
                if(SwitchPetView.instance != this)
                {
                    Destroy(this);
                }
            }
        }



        void Start()
        {
            /* Get the transform of the main camera */
            camera_transform = Camera.main.transform;
        }



        void Update()
        {
            
            if(camera_moving_direction == LEFT)
            {
                /* Moving camera for left */
                if(camera_transform.position.x > -1 * ViewWidth * 0.8f)
                {
                    camera_transform.Translate(-1 * camera_transform.right * ViewMovingSpeed * Time.deltaTime);
                }
            }
            else if(camera_moving_direction == RIGHT)
            {
                /* Moving camera for right */
                if(camera_transform.position.x < ViewWidth * 0.8f)
                {
                    camera_transform.Translate(camera_transform.right * ViewMovingSpeed * Time.deltaTime);
                }
            }
        }

        #endregion
    }
}

