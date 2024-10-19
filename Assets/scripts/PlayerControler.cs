using System;
using RPG.Movement;
using RPG.Combat;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerControler : MonoBehaviour
    {

        private void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
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
                if (target == null)
                {
                    continue;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Atack(target);
                }
                return;

            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0)) // когда зажата
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

    }

}