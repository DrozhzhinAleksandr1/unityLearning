using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Sinematics
{
    public class CinematicsControllRemover : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControll;
            GetComponent<PlayableDirector>().stopped += EnableControll;
        }

        void DisableControll(PlayableDirector pd)
        {
            print("DisableControll");
        }

        void EnableControll(PlayableDirector pd)
        {
            print("EnableControll");

        }
    }

}