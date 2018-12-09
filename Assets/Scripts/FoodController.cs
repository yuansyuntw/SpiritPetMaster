using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class FoodController : MonoBehaviour
    {

        public float FoodHeight;
        public List<GameObject> FoodList;

        //public Vector2 FoodSize = new Vector2(150,150);
        

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
                Vector2 pos = Camera.main.transform.position;
                pos.y += FoodHeight;

                Instantiate(FoodList[_index], pos, Quaternion.identity, transform);
            }
        }
        public void InstanceFood(GameObject _food)
        {
                Vector2 pos = Camera.main.transform.position;
                pos.y += FoodHeight;

                Debug.Log("123");
                Instantiate(_food, pos, Quaternion.identity, transform);
        }

    }
}


