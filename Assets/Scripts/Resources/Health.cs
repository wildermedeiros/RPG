using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;
using GameDevTV.Utils;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 80f;

        LazyValue<float> healthPoints;
        bool isDead = false;
        BaseStats baseStats;

        private void Awake() 
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void OnEnable() {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable() {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * (regenerationPercentage / 100f);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints); 
        }

        public bool IsDead()
        {
            return isDead;    
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
//            print(gameObject.name + " took damage: " + damage);

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0); 
            if (healthPoints.value == 0)
            {
                Die();
                AwardExperiencePoints(instigator);
            }
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public float GetPercentage()
        {
            return 100 * (healthPoints.value / GetComponent<BaseStats>().GetStat(Stat.Health));
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

            float experiencePoints = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
            experience.GainExperience(experiencePoints);
        }

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if (healthPoints.value <= 0)
            {
                Die();
            }
        }
    }
}