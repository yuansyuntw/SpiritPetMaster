using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class Pet
    {
        public int PetID;       //寵物種類代號
        public Sprite PetSprite;
        public string Name;
        public int Level = 1;
        public int Mood = 0;
        public int Hunger = 10;

        public Pet(int _id)
        {
            PetID = _id;
            Name = _id.ToString();
        }
    }
}


