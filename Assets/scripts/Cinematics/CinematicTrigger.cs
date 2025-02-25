using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alredyTriggered = false;
        void OnTriggerEnter(Collider other)
        {
            if (alredyTriggered) return;
            if (other.gameObject.tag != "Player") return;

            alredyTriggered = true;
            GetComponent<PlayableDirector>().Play();
        }
    }

}
