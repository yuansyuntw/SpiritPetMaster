using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class FoodController : MonoBehaviour
    {

        public float FoodHeight;
        public List<GameObject> FoodList;

        

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }


        public void InstanceFood(int _index)
        {
            if((0 <= _index) && (_index < FoodList.Count))
            {
                Vector3 pos = Camera.main.transform.position;
                pos.y += FoodHeight;

                Instantiate(FoodList[_index], pos, Quaternion.identity, transform);
            }
        }
    }
}


