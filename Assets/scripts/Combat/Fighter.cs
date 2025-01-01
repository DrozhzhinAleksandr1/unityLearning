using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2.5f;
        [SerializeField] float timeBeetwenAnnacks = 1f;
        [SerializeField] float weaponDamage = 5f;
        Health target;

        float timeScinceLastAttack = 0;
        private void Update()
        {
            timeScinceLastAttack += Time.deltaTime;
            // print($"timeScinceLastAttack: {timeScinceLastAttack}");
            if (target != null)
            {
                if (target.IsDead()) return;

                float currentDistance = Vector3.Distance(transform.position, target.transform.position);
                bool isInRange = currentDistance < weaponRange;
                if (!isInRange)
                {
                    // print(currentDistance);
                    GetComponent<Mover>().MoveTo(target.transform.position);
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
            if (target == null) return;
            target.TakeDamage(weaponDamage);

        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeBeetwenAnnacks < timeScinceLastAttack)
            {
                TriggerAttack();
                timeScinceLastAttack = 0;
                // this will triger the hit event void Hit()
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Atack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            // print("Bang bang");
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }

}