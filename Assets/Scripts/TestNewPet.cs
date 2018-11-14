using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class TestNewPet : MonoBehaviour
    {
        public string PetKind;
        public string PetName;

        public void New()
        {
            PetViewController.instance.NewPetView(PetKind, PetName);
        }

    }
}


