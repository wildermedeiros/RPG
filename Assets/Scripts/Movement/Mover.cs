using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Attributes;
using UnityEngine.Events;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        NavMeshAgent navMeshAgent;
        Animator animator;
        Health health; 
        
        [SerializeField] float maxSpeed = 10;
        [SerializeField] float maxNavPathLenght = 40f;
        [SerializeField] UnityEvent onLFootMovement;
        [SerializeField] UnityEvent onRFootMovement;

        private void Awake() 
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            health = GetComponent<Health>();
        }

        void Start()
        {
            animator.enabled = false; 
            animator.enabled = true; 
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public bool CanMoveTo(Vector3 destination)
        {
            NavMeshPath path = new NavMeshPath();
            bool hasPath = NavMesh.CalculatePath(transform.position, destination, NavMesh.AllAreas, path);
            if (!hasPath) { return false; }
            if (path.status != NavMeshPathStatus.PathComplete) { return false; }

            if (GetPathLenght(path) > maxNavPathLenght) { return false; }

            return true;
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public void LFootEvent()
        {
            //onLFootMovement.Invoke();
            print("Left movement");
        }

        public void RFootEvent()
        {
            onRFootMovement.Invoke();
            print("Right movement");
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity).normalized;
            float speed = localVelocity.z;

            animator.SetFloat("forwardSpeed", speed);
        }

        private float GetPathLenght(NavMeshPath path)
        {
            float totalDistance = 0;
            if (path.corners.Length > 2) { return totalDistance; }

            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                totalDistance += Vector3.Distance(path.corners[i], path.corners[i + 1]);
            }
            return totalDistance;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 position = (SerializableVector3)state;
            GetComponent<NavMeshAgent>().Warp(position.ToVector());
        }
    }
}
