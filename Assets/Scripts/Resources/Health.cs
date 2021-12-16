using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 80f;

        float healthPoints = -1;
        float experiencePoints; 
        bool isDead = false;

        private void Start() 
        {
            if(healthPoints < 0) 
            {  
                healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }   
            experiencePoints = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);

            BaseStats baseStats = GetComponent<BaseStats>();
            baseStats.onLevelUp += RegenerateHealth;
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100f);
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints); 
        }

        public bool IsDead()
        {
            return isDead;    
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);

            healthPoints = Mathf.Max(healthPoints - damage, 0); 
            if (healthPoints == 0)
            {
                Die();
                AwardExperiencePoints(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health));
        }

        private void Die()
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

            if (healthPoints <= 0)
            {
                Die();
            }
        }
    }
}