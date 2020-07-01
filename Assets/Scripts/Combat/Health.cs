using System;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;    
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0); 
            if (healthPoints == 0)
            {
                DieBehaviour();
            }
        }

        private void DieBehaviour()
        {
            if (isDead) return;
            
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}