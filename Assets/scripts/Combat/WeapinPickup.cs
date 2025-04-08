using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Combat
{
    public class WeapinPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        void OnTriggerEnter(Collider other)
        {
            // character must have ridgit body
            // sphere colider must haave checkbox  => is Trigger
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }

    }
}
