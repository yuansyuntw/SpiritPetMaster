using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class Pet : MonoBehaviour
    {
        public int ID;                          //寵物代號(唯一)
        public int Kind;                        //寵物種類
        public string Name;
        public int Level = 1;
        public int Mood = 90;
        public int Hunger = 90;

        public string PetSpriteName;            //圖片資料
        public string PetTalkingFilename;       //寵物說話檔案位置
        public string[] PetTakingContents;      //寵物說話內容

        public float speed = 1;
        public float maxHP = 100;
        public float maxMP = 100;
        public float MPRecover = 0.005f;
        public float HPRecover = 0.005f;
        public int PetfireAttack = 100;
        public int PetwaterAttack = 100;
        public int PetwindAttack = 100;



        /*  */
        public Pet()
        {

        }



        /* Load a pet */
        public void LoadPet(int _id)
        {
            ID = _id;

            LoadData();
        }



        public void NewPet(int _id, int _kind, string _name)
        {
            ID = _id;
            Kind = _kind;
            Name = _name;
            PetSpriteName = "pet_image" + Kind.ToString();
            PetTalkingFilename = "pet_taking" + Kind.ToString();

            SaveData();
        }



        protected void LoadData()
        {
            string petid = ID.ToString();
            PetSpriteName = PlayerData.instance.LoadData<string>(petid, "pet_image_name");
            PetTalkingFilename = PlayerData.instance.LoadData<string>(petid, "pet_taking_filename");
            Name = PlayerData.instance.LoadData<string>(petid, "pet_name");
            Kind = PlayerData.instance.LoadData<int>(petid, "pet_kind");
            Level = PlayerData.instance.LoadData<int>(petid, "pet_level");
            Mood = PlayerData.instance.LoadData<int>(petid, "pet_mood");
            Hunger = PlayerData.instance.LoadData<int>(petid, "pet_hunger");
        }



        protected void SaveData()
        {
            string petid = ID.ToString();
            PlayerData.instance.SaveData<string>(petid, "pet_image_name", PetSpriteName);
            PlayerData.instance.SaveData<string>(petid, "pet_taking_filename", PetTalkingFilename);
            PlayerData.instance.SaveData<string>(petid, "pet_name", Name);
            PlayerData.instance.SaveData<int>(petid, "pet_king", Kind);
            PlayerData.instance.SaveData<int>(petid, "pet_level", Level);
            PlayerData.instance.SaveData<int>(petid, "pet_mood", Mood);
            PlayerData.instance.SaveData<int>(petid, "pet_hunger", Hunger);
        }



        
    }
}


