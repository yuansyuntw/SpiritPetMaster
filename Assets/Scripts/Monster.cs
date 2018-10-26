using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritMonsterMaster
{
    public class Monster : MonoBehaviour
    {
        public int MonsterID;       //寵物種類代號
        public Sprite MonsterSprite;
        public string Name;
        public int Level = 1; 

        public float speed = 1;
        public float maxHP = 100;
        //public float maxMP = 100;
        //public float MPRecover = 0.005f;
        public float HPRecover = 0.005f;
        public int Monsterfire, Monsterwater, Monsterwind;
        public float warning;
        public float Attacknum;


        public Monster(int _id)
        {
            MonsterID = _id;
            Name = _id.ToString();
        }
    }
}