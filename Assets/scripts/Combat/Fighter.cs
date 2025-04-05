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
        [SerializeField] Transform handTransform = null;
        [SerializeField] Weapon weapon = null;
        Health target;

        float timeScinceLastAttack = Mathf.Infinity;

        void Start()
        {
            SpawnWeapon();
        }
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
                    GetComponent<Mover>().MoveTo(target.transform.position, 1f);
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

        void SpawnWeapon()
        {
            if (weapon == null) return;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(handTransform, animator);
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
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }

}