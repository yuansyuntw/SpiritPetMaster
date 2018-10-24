using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class Pet : MonoBehaviour
    {
        public int PetID;       //寵物種類代號
        public Sprite PetSprite;
        public string Name;
        public int Level = 1;
        public int Mood = 0;
        public int Hunger = 10;

        public float speed = 1;
        public float maxHP = 100;
        public float maxMP = 100;
        public float MPRecover = 0.005f;
        public float HPRecover = 0.005f;
        public int PetfireAttack = 100;
        public int PetwaterAttack = 100;
        public int PetwindAttack = 100;


        public Pet(int _id)
        {
            PetID = _id;
            Name = _id.ToString();
        }
    }
}


