using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefabe = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] float weaponRange = 2.5f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (weaponPrefabe != null)
            {
                Transform handTransform = GetHandTranform(rightHand, leftHand);
                GameObject weapon = Instantiate(weaponPrefabe, handTransform);
                weapon.name = weaponName;
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetHandTranform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded)
            {
                handTransform = rightHand;
            }
            else
            {
                handTransform = leftHand;
            }

            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Transform handTransform = GetHandTranform(rightHand, leftHand);
            Projectile projectileInstance = Instantiate(projectile, handTransform.position, Quaternion.identity);
            projectileInstance.SetTarget(target, weaponDamage);
        }

        public float getWeaponRange()
        {
            return weaponRange;
        }

        public float getWeaponDamage()
        {
            return weaponDamage;
        }

    }
}