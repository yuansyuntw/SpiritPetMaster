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

        public string PetSpriteName;            //寵物圖片
        public string PetAnimatorName;          //寵物動畫名稱
        public string PetTalkingFilename;       //寵物說話檔案位置
        public string[] PetTakingContents;      //寵物說話內容

        public float Speed = 1;
        public float MaxHP = 100;
        public float MaxMP = 100;
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
            PetSpriteName = "Pet" + Kind.ToString() + "/idle/idle1";
            PetAnimatorName = "Pet" + Kind.ToString() + "/pet" + Kind.ToString();
            PetTalkingFilename = "pet_taking" + Kind.ToString();

            SaveData();
        }



        protected void LoadData()
        {
            string petid = ID.ToString();
            PetSpriteName = PlayerData.instance.LoadData<string>(petid, "pet_image_name");
            PetAnimatorName = PlayerData.instance.LoadData<string>(petid, "pet_animator_name");
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
            PlayerData.instance.SaveData<string>(petid, "pet_animator_name", PetAnimatorName);
            PlayerData.instance.SaveData<string>(petid, "pet_taking_filename", PetTalkingFilename);
            PlayerData.instance.SaveData<string>(petid, "pet_name", Name);
            PlayerData.instance.SaveData<int>(petid, "pet_king", Kind);
            PlayerData.instance.SaveData<int>(petid, "pet_level", Level);
            PlayerData.instance.SaveData<int>(petid, "pet_mood", Mood);
            PlayerData.instance.SaveData<int>(petid, "pet_hunger", Hunger);

            PlayerData.instance.SaveData<string>(petid, "pet_speed", Speed.ToString());
            PlayerData.instance.SaveData<string>(petid, "pet_maxHP", MaxHP.ToString());
            PlayerData.instance.SaveData<string>(petid, "pet_maxMP", MaxMP.ToString());
            PlayerData.instance.SaveData<string>(petid, "pet_mPRecover", MPRecover.ToString());
            PlayerData.instance.SaveData<string>(petid, "pet_hPRecover", HPRecover.ToString());
            PlayerData.instance.SaveData<int>(petid, "pet_petfireAttack", PetfireAttack);
            PlayerData.instance.SaveData<int>(petid, "pet_petwaterAttack", PetwaterAttack);
            PlayerData.instance.SaveData<int>(petid, "pet_petwindAttack", PetwindAttack);
        
    }



        
    }
}


