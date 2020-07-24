using UnityEngine;
using System.Collections;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;
using System;

// TODO checar o porque da animação da caveirinha não estar rolando

namespace RPG.Control
{
    public class IAController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f; 

        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;

        //state like, memory for the guard comeback in his guard position
        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity; 

        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");


            //maybe a retunr over here in case of this enemy isn't doing the guard behaviour
            guardPosition = transform.position;
        }

        private void Update()
        {
            if (health.IsDead()) { return; }

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player.gameObject))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                GuardBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player.gameObject);
        }

        private IEnumerator SuspicioningThePlayer()
        {
            yield return new WaitForSeconds(suspicionTime);
        }

        private bool InAttackRangeOfPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position) < chaseDistance;
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}