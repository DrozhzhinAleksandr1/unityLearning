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

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            if (weaponPrefabe != null)
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