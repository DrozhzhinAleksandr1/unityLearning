using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 4f;
        [SerializeField] float timeBeetwenAnnacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Transform target;

        float timeScinceLastAttack = 0;
        private void Update()
        {
            timeScinceLastAttack += Time.deltaTime;
            // print($"timeScinceLastAttack: {timeScinceLastAttack}");
            if (target != null)
            {

                float currentDistance = Vector3.Distance(transform.position, target.position);
                bool isInRange = currentDistance < weaponRange;
                if (!isInRange)
                {
                    // print(currentDistance);
                    GetComponent<Mover>().MoveTo(target.position);
                }
                else
                {
                    GetComponent<Mover>().Stop();
                    // target = null;
                    AttackBehaviour();
                }
            }
        }

        // Animation event
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);

        }

        private void AttackBehaviour()
        {
            if (timeBeetwenAnnacks < timeScinceLastAttack)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeScinceLastAttack = 0;
                // this will triger the hit event void Hit()
            }
        }

        public void Atack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            // print("Bang bang");
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

    }

}