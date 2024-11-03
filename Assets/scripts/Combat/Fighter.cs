using UnityEngine;
using RPG.Movement;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] float weaponRange = 4f;
        Transform target;
        private void Update()
        {
            if (target != null)
            {

                float currentDistance = Vector3.Distance(transform.position, target.position);
                bool isInRange = currentDistance < weaponRange;
                if (!isInRange)
                {
                    print(currentDistance);
                    GetComponent<Mover>().MoveTo(target.position);
                }
                else
                {
                    GetComponent<Mover>().Stop();
                    // target = null;
                }
            }
        }
        public void Atack(CombatTarget combatTarget)
        {
            // print("Bang bang");
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }
    }

}