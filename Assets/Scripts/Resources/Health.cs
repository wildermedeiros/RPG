using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        float experiencePoints; 
        bool isDead = false;

        private void Start() 
        {
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            experiencePoints = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
        }

        public bool IsDead()
        {
            return isDead;    
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0); 
            if (healthPoints == 0)
            {
                //instigator.GetComponent<ActionScheduler>().CancelCurrentAction();
                DieBehaviour();
                AwardExperiencePoints(instigator);
            }
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void DieBehaviour()
        {
            if (isDead) return; 
            
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AwardExperiencePoints(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) { return; }

            experience.GainExperience(experiencePoints);
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints == 0)
            {
                DieBehaviour();
            }
        }
    }
}