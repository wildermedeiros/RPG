using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float distanteRange = 2f;
        [SerializeField] float timeBetweenAttacks = 2f;
        [SerializeField] float weaponDamge = 10f;
        
        Transform target;
        float timeSinceLastAttack = 0f;
        
        private void Update()
        {
            if (target == null) return;

            CheckDistance();
        }

        private void CheckDistance()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                if (timeSinceLastAttack >= timeBetweenAttacks)
                {
                    AttackBehaviour();
                    timeSinceLastAttack = 0f;
                }
            }
        }

        private void AttackBehaviour()
        {
            // This will trigger the HitEvent() in animation event.
            GetComponent<Animator>().SetTrigger("attack");
        }

        public void HitEvent() // Animation event reference
        {
           Health healthComponent = target.GetComponent<Health>();
           healthComponent.TakeDamage(weaponDamge);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.position, transform.position) <= distanteRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            GetComponent<ActionScheduler>().StartAction(this);
        }

        public void Cancel(){
            target = null;
        }


    }
}
