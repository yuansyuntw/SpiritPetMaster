using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class PlayerData : MonoBehaviour
    {

        static public PlayerData instance;

        private List<Pet> OwnPets = new List<Pet>();

        #region pet view 
        private int currentPetViewIndex = 0;
        #endregion

        #region public function
        public List<Pet> PetViewGetPets()
        {
            return OwnPets;
        }


        public Pet PetViewGetCurrentPetData()
        {
            Debug.LogFormat("current pet view index: {0}/{1}", currentPetViewIndex, OwnPets.Count);
            if(OwnPets.Count > 0)
            {
                return OwnPets[currentPetViewIndex];
            }
            else
            {
                return null;
            }
        }

        public Pet PetViewGetNextPetData()
        {
            if(currentPetViewIndex + 1 < OwnPets.Count)
            {
                currentPetViewIndex += 1;
            }
            return PetViewGetCurrentPetData();
        }

        public Pet PetViewGetLastPetData()
        {
            if (currentPetViewIndex - 1 >= 0)
            {
                currentPetViewIndex -= 1;
            }
            return PetViewGetCurrentPetData();
        }
        #endregion

        void Awake()
        {
            if (PlayerData.instance == null)
            {
                PlayerData.instance = this;
            }
            else
            {
                if (PlayerData.instance != this)
                {
                    Destroy(this);
                }
            }

            /* DEBUG */
            int DEBUG_PET_NUMBER = 5;
            for (int i = 0; i < DEBUG_PET_NUMBER; i++)
            {
                OwnPets.Add(new Pet());
            }
        }

        void Start()
        {
            
        }

        
    }
}


