using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpiritPetMaster
{
    public class SceneManager : MonoBehaviour
    {

        public void ChangeScene(string _scene)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(_scene);
        }
    }
}

