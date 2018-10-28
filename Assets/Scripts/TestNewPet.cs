using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class TestNewPet : MonoBehaviour
    {
        public int PetKing;
        public string PetName;

        public void New()
        {
            PetViewController.instance.NewPetView(PetKing, PetName);
        }

    }
}


