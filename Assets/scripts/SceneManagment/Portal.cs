using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagment
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;
        void OnTriggerEnter(Collider other)
        {
            // print("Portal triggered");
            if (other.tag != "Player") return;
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
