using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass character in characterClasses)
            {
                if(character.characterClass == characterClass)
                {
                    //return character.health[level - 1];
                }
            }
            return 0;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] progressionStats; 
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stats;
            public float[] levels; 
        }
    }
}