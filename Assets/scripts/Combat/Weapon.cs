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

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (weaponPrefabe != null)
            {
                Instantiate(weaponPrefabe, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
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