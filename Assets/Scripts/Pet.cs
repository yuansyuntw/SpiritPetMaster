using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    [System.Serializable]
    public class Pet
    {
        public int PetID;                       //寵物種類代號
        public string PetName;
        public int Level = 1;
        public int Mood = 0;
        public int Hunger = 1;

        public string PetSpriteName;            //圖片資料
        public string PetTalkingFilename;       //寵物說話檔案位置
        public string[] PetTakingContents;      //寵物說話內容

        public Pet(int _id)
        {
            PetID = _id;
            PetName = _id.ToString();
        }
    }
}


