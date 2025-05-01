using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class WeapinPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 10;

        void OnTriggerEnter(Collider other)
        {
            // character must have ridgit body
            // sphere colider must haave checkbox  => is Trigger
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                // Destroy(gameObject); // changing this on HideForSeconds
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            // transform.GetChild(0).gameObject.SetActive(shouldShow); // hide game object
            foreach (Transform child in transform) // get all childs
            {
                child.gameObject.SetActive(shouldShow); // hide game object
            }
        }
    }
}
