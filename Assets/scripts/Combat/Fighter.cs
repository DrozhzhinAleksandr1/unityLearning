using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBeetwenAnnacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        Health target;
        Weapon currentWeapon = null;

        float timeScinceLastAttack = Mathf.Infinity;

        void Start()
        {
            EquipWeapon(defaultWeapon);
        }
        private void Update()
        {
            timeScinceLastAttack += Time.deltaTime;
            // print($"timeScinceLastAttack: {timeScinceLastAttack}");
            if (target != null)
            {
                if (target.IsDead()) return;

                float currentDistance = Vector3.Distance(transform.position, target.transform.position);
                bool isInRange = currentDistance < currentWeapon.getWeaponRange();
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
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.getWeaponDamage());
            }

        }
        // Animation event
        void Shoot()
        {
            Hit();
        }
        // Animation event

        public void EquipWeapon(Weapon weapon)
        {
            // if (weapon == null) return;
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
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