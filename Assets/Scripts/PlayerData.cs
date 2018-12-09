using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BayatGames.SaveGameFree;

namespace SpiritPetMaster
{
    public class PlayerData : MonoBehaviour
    {
        static public PlayerData instance;
        public string PlayerName;

        const int foodsNumber = 6;

        #region Private Save Data    
        [SerializeField]
        List<PetView> OwnPets = new List<PetView>();
        int current_focus_petid = -1;

        int[] foods = new int[foodsNumber];
        #endregion

        #region public function

        public void ClearPlayerData()
        {
            for(int i = 0; i < OwnPets.Count; i++)
            {
                Destroy(OwnPets[i].gameObject);
            }
            OwnPets.Clear();
            SaveGame.Clear();
        }



        public void SetPetViews(List<PetView> _petviews)
        {
            OwnPets = _petviews;
        }


        public void SaveData<T>(string _petid, string _property,T _value)
        {
            Save<T>(PlayerName, _petid, _property, _value);
        }



        public T LoadData<T>(string _petid, string _property)
        {
            return Load<T>(PlayerName, _petid, _property);
        }


        public string[] GetPetsId()
        {
            List<string> ids = new List<string>();
            int pet_count = Load<int>(PlayerName, "pet_count");
            if (pet_count > 0)
            {
                string pets_id = Load<string>(PlayerName, "pets_id");
                Debug.LogFormat("pets id: {0}", pets_id);
                string[] temp = pets_id.Split(' ');
                for(int i=0;i< pet_count; i++)
                {
                    ids.Add(temp[i]);
                }
            }

            return ids.ToArray();
        }



        public void SavePlayerData()
        {
            /* Save all of the property of the pet */
            string pets_id = "";
            for (int i = 0; i < OwnPets.Count; i++)
            {
                string id = OwnPets[i].ID.ToString();
                pets_id += id + " ";
            }

            /* Save all of the id of pet */
            Save<int>(PlayerName, "pet_count", OwnPets.Count);
            Save<string>(PlayerName, "pets_id", pets_id);
            Save<int>(PlayerName, "current_focus_petid", current_focus_petid);

            for(int i=0;i<foodsNumber;++i)
                Save<int>(PlayerName, "foods"+i.ToString() , foods[i]);

            // Debug.LogFormat("pet count:{0}, ids: {1}", OwnPets.Count, pets_id);
        }

        public void SavePlayerFocusPetId(int _petid)
        {
            current_focus_petid = _petid;
            SavePlayerData();
        }

        public int GetPlayerFocusPetId()
        {
            return Load<int>(PlayerName, "current_focus_petid");
        }

        public int GetFood(int ind)
        {
            return Load<int>(PlayerName, "foods"+ind.ToString());
        }
        #endregion



        #region life time
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
        }



        void Start()
        {
            
        }



        void OnDisable()
        {
            /* When the game closing, the script will auto saing the player's pet data */
            SavePlayerData();
        }
        #endregion



        #region save and load data

        void Save<T>(string _player_name, string _property_name, T _value)
        {
            SaveGame.Save<T>(_player_name + "_" + _property_name, _value);
        }



        void Save<T>(string _player_name, string _petid, string _property_name, T _value)
        {
            SaveGame.Save<T>(_player_name + "_" + _petid + "_" + _property_name, _value);
        }

        T Load<T>(string _player_name, string _property_name)
        {
            return SaveGame.Load<T>(_player_name + "_" + _property_name);
        }



        T Load<T>(string _player_name, string _petid, string _property_name)
        {
            return SaveGame.Load<T>(_player_name + "_" + _petid + "_" + _property_name);
        }

        #endregion
    }
}


