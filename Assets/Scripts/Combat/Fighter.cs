using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 2f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null; 
        [SerializeField] Weapon defaultWeapon = null;
        
        Health target;
        float timeSinceLastAttack = Mathf.Infinity;
        Weapon currentWeapon; 

        private void Start() 
        {
            EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            CheckDistance();
        }

        private void CheckDistance()
        {   
            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            // This will trigger the HitEvent() in animation event.
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0f;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        // Animation event reference
        public void HitEvent() 
        {
            if(target == null) return;

            if(currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            } 
            else if (currentWeapon.HasMultiProjectiles())
            {
                currentWeapon.LaunchMultiProjectiles(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetDamage());
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.transform.position, transform.position) <= currentWeapon.GetRange();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null) return false;  // TODO i think this is unnecesseray 
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, defaultWeapon.GetRange());
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }
    }
}
