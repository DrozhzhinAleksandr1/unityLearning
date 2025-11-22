using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Sinematics
{
    public class CinematicsControllRemover : MonoBehaviour
    {
        GameObject player;
        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControll;
            GetComponent<PlayableDirector>().stopped += EnableControll;
        }

        void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControll;
            GetComponent<PlayableDirector>().stopped -= EnableControll;
        }

        void DisableControll(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerControler>().enabled = false;
        }

        void EnableControll(PlayableDirector pd)
        {
            player.GetComponent<PlayerControler>().enabled = true;
        }
    }

}