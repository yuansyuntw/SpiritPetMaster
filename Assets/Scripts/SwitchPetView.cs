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



        public void FocusPet(int _view_index)
        {
            
        }

        #endregion


        #region life cycle

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

