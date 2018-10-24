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

        #region Save Data    
        [SerializeField]
        List<Pet> OwnPets = new List<Pet>();
        [SerializeField]
        int currentPetViewIndex = 0;
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

        public void SaveAndReloadPlayerData()
        {
            SavePlayerData();
            LoadPlayerData();
        }

        public void NewPet(int _id)
        {
            Pet pet = new Pet(_id);
            OwnPets.Add(pet);

            SaveAndReloadPlayerData();
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

            LoadPlayerData();
        }

        void Start()
        {
            
        }

        void OnDestroy()
        {
            SavePlayerData();
        }
        #endregion

        void SavePlayerData()
        {
            /* Save all of the property of the pet */
            string pets_id = "";
            for(int i=0;i<OwnPets.Count;i++)
            {
                string id = OwnPets[i].PetID.ToString();
                pets_id += id + " ";

                Save<string>(PlayerName, id, "pet_image_name", OwnPets[i].PetSpriteName);
                Save<string>(PlayerName, id, "pet_name", OwnPets[i].PetName);
                Save<int>(PlayerName, id, "pet_level", OwnPets[i].Level);
                Save<int>(PlayerName, id, "pet_mood", OwnPets[i].Mood);
                Save<int>(PlayerName, id, "pet_hunger", OwnPets[i].Hunger);
            }

            /* Save all of the id of pet */
            Save<string>(PlayerName, "pets_id", pets_id);

            /* Save current the viewed pet index */
            Save<int>(PlayerName, "current_view_index", currentPetViewIndex);
        }



        void Save<T>(string _player_name, string _property_name, T _value)
        {
            SaveGame.Save<T>(_player_name + "_" + _property_name, _value);
        }



        void Save<T>(string _player_name, string _petid, string _property_name, T _value)
        {
            SaveGame.Save<T>(_player_name + "_" + _petid + "_" + _property_name, _value);
        }

        

        void LoadPlayerData()
        {
            OwnPets.Clear();

            string pets_id = Load<string>(PlayerName, "pets_id");
            string[] ids = pets_id.Split(' ');

            /* Loading all of the player's pet */
            for(int i=0;i<ids.Length;i++)
            {
                int id;
                int.TryParse(ids[i], out id);
                Pet pet = new Pet(id);
                pet.PetSpriteName = Load<string>(PlayerName, ids[i], "pet_image_name");
                pet.name = Load<string>(PlayerName, ids[i], "pet_name");
                pet.Level = Load<int>(PlayerName, ids[i], "pet_level");
                pet.Mood = Load<int>(PlayerName, ids[i], "pet_mood");
                pet.Hunger = Load<int>(PlayerName, ids[i], "pet_hunger");
                OwnPets.Add(pet);
            }
        }



        T Load<T> (string _player_name, string _property_name)
        {
            return SaveGame.Load<T>(_player_name + "_" + _property_name);
        }



        T Load<T>(string _player_name, string _petid, string _property_name)
        {
            return SaveGame.Load<T>(_player_name + "_" + _petid + "_" + _property_name);
        }
    }
}


