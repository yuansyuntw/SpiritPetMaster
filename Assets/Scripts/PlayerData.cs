using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using BayatGames.SaveGameFree;

namespace SpiritPetMaster
{
    public class PlayerData : MonoBehaviour
    {
        static public PlayerData instance;
        public string PlayerName;

        #region Private Save Data
        [SerializeField]
        List<PetView> OwnPets = new List<PetView>();
        int current_focus_petid = -1;

        public int[] foodsCounter = new int[FoodController.foodsNumber];
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


        public void SetPlayerName(string _player_name)
        {
            PlayerName = _player_name;
        }
        public void SetPlayerName(Text _text)
        {
            PlayerName = _text.text;
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


        public void SaveAllPets()
        {
            int pet_count = Load<int>(PlayerName, "pet_count");
            string old_pets_id = Load<string>(PlayerName, "pets_id");
            string[] old_pets_idlist = new string[0];
            if(old_pets_id==null)
                old_pets_id="";
            old_pets_idlist = old_pets_id.Split(' ');

            for (int i = 0; i < OwnPets.Count; i++)
            {
                string id = OwnPets[i].ID.ToString();
                if( ! old_pets_idlist.Contains(id) )
                {
                    old_pets_id += " "+id;
                    old_pets_idlist = old_pets_id.Split(' ');
                }
            }
            
            /* Save all of the id of pet */
            Save<int>(PlayerName, "pet_count", OwnPets.Count);
            Save<string>(PlayerName, "pets_id", old_pets_id);
        }
        public bool AddPet(int id)
        {
            string _id = id.ToString();
            List<string> ids = new List<string>();
            int pet_count = Load<int>(PlayerName, "pet_count");
            string pets_id = Load<string>(PlayerName, "pets_id");
            if(pets_id==null)
                pets_id = "";
            Debug.LogFormat("pets id: {0}", pets_id);
            string[] temp = new string[0];
            temp = pets_id.Split(' ');
            for(int i=0;i< pet_count; i++)
            {
                ids.Add(temp[i]);
            }
            if(!temp.Contains(_id))
            {
                pet_count += 1;
                if(pets_id=="")
                    pets_id += _id;
                else
                    pets_id = pets_id + " " + _id;
                
                Save<int>(PlayerName, "pet_count", OwnPets.Count);
                Debug.Log(pets_id);
                Save<string>(PlayerName, "pets_id", pets_id);
            }
            else
                return false;
            return true;
        }
        public bool DeletePet(string _id)
        {
            List<string> ids = new List<string>();
            int pet_count = Load<int>(PlayerName, "pet_count");
            string pets_id = Load<string>(PlayerName, "pets_id");
            if(pets_id==null)
                pets_id="";
            string[] temp = pets_id.Split(' ');
            for(int i=0;i< pet_count; i++)
            {
                if(temp[i]!=_id)
                    ids.Add(temp[i]);
            }
            if(temp.Contains(_id))
            {
                pet_count -= 1;
                Save<int>(PlayerName, "pet_count", OwnPets.Count);
                Save<string>(PlayerName, "pets_id", pets_id);
            }
            else
                return false;
                
            return true;
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
            Debug.Log(ids.ToArray().Length);
            return ids.ToArray();
        }

        public void SaveFood(int _ind)
        {
            if(_ind<0 || _ind>=FoodController.foodsNumber)
                return;
            Save<int>(PlayerName, "foods"+_ind.ToString(), foodsCounter[_ind]);
        }
        public int GetFoodNum(int _ind)
        {
            if(_ind<0 || _ind>=FoodController.foodsNumber)
                return -1;
            return foodsCounter[_ind] = Load<int>(PlayerName, "foods"+_ind.ToString());
        }
        public void AddFood(int _ind, int _add)
        {
            if(_ind<0 || _ind>=FoodController.foodsNumber)
                return;
            foodsCounter[_ind] += _add;
            SaveFood(_ind);
        }
        public void AddFood(int _ind)
        {
            if(_ind<0 || _ind>=FoodController.foodsNumber)
                return;
            foodsCounter[_ind] += 1;
            SaveFood(_ind);
        }
        public void SaveFoods()
        {
            for(int i=0;i<FoodController.foodsNumber;++i)
                SaveFood(i);
        }
        public int[] GetFoods()
        {
            for(int i=0;i<FoodController.foodsNumber;++i)
                GetFoodNum(i);
            return foodsCounter;
        }
        public void LoadFoods()
        {
            for(int i=0;i<FoodController.foodsNumber;++i)
                GetFoodNum(i);
        }

        public void SavePlayerData()
        {
            if(OwnPets.Count > 0)
            {
                /* Save all of the property of the pet */
                SaveAllPets();
                Save<int>(PlayerName, "current_focus_petid", current_focus_petid);
                SaveFoods();
            }
        }

        public void SavePlayerFocusPetId(int _petid)
        {
            current_focus_petid = _petid;
            Debug.Log(current_focus_petid);
            Save<int>(PlayerName, "current_focus_petid", current_focus_petid);
        }

        public int GetPlayerFocusPetId()
        {
            return Load<int>(PlayerName, "current_focus_petid");
        }

        public void AddNewPet(string PetKind, string PetName)
        {
            PetViewController.instance.NewPetView(PetKind, PetName);
        }

        #endregion



        #region life time
        void Awake()
        {
            if (PlayerData.instance == null)
            {
                PlayerData.instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                if (PlayerData.instance != null &&  PlayerData.instance != this)
                {
                    Destroy(gameObject);
                }
            }
            //DontDestroyOnLoad(this);
        }



        void Start()
        {
            Debug.Log(PlayerName);
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


