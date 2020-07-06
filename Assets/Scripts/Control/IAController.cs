using UnityEngine;

namespace RPG.Control
{
    public class IAController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f; 

        private void Update()
        {
            if (DistanceToPlayer() <= chaseDistance)
            {
                print("Start chasing by" + gameObject.name);
            }
        }

        private float DistanceToPlayer()
        {
            GameObject player = GameObject.FindWithTag("Player");
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}