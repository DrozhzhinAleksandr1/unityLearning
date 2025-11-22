using System;
using RPG.Movement;
using RPG.Combat;
using UnityEngine;
using RPG.Core;
using RPG.Attributes;

namespace RPG.Control
{
    public class PlayerControler : MonoBehaviour
    {

        Health health;

        void Awake()
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit item in hits)
            {
                // string jsonString = JsonUtility.ToJson(item, true);
                // Debug.Log(jsonString);
                // print(item.collider.name);
                // if (item.collider.name == "Enemy") // first try
                // {
                //     GetComponent<Fighter>().Atack();
                // }

                CombatTarget target = item.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) // if (target.gameObject == null)
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Atack(target.gameObject);
                }
                return true;

            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private bool InteractWithMovement()
        {

            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0)) // когда зажата
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                    // GetComponent<Fighter>().Cancel();
                }
                return true;
            }
            return false;
        }

    }

}