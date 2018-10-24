using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class Pet : MonoBehaviour
    {
        public int PetID;               //寵物種類代號

        public string PetSpriteName;    //圖片資料

        public string PetName;

        public int Level = 1;
        public int Mood = 0;
        public int Hunger = 10;

        public Pet(int _id)
        {
            PetID = _id;
            PetName = _id.ToString();
        }
    }
}


