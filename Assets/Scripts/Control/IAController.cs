using UnityEngine;
using RPG.Combat;
using RPG.Movement;
using RPG.Core;

// TODO checar o porque da animação da caveirinha não estar rolando

namespace RPG.Control
{
    public class IAController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;

        GameObject player;
        Fighter fighter;
        Health health;

        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (health.IsDead()) { return; }

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player.gameObject))
            {
                GetComponent<Fighter>().Attack(player.gameObject);
            }
            else
            {
                GetComponent<Fighter>().Cancel();
            }
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