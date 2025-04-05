using UnityEngine;


namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefabe = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;

        public void Spawn(Transform handTransform, Animator animator)
        {
            Instantiate(weaponPrefabe, handTransform);
            animator.runtimeAnimatorController = animatorOverride;
        }

    }
}