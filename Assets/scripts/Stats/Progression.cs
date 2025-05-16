using UnityEngine;

namespace RPG.Stats
{

    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if (progressionClass.characterClass != characterClass) continue;

                foreach (ProghressionStat proghressionStat in progressionClass.stats)
                {
                    if (proghressionStat.stat != stat) continue;
                    if (proghressionStat.levels.Length < level) continue;
                    return proghressionStat.levels[level - 1];
                }
            }

            return 0;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProghressionStat[] stats;
        }

        [System.Serializable]
        class ProghressionStat
        {
            public Stat stat;
            public float[] levels;

        }
    }
}