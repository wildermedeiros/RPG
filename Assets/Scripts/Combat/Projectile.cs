using RPG.Resources;
using UnityEngine;

// TODO IDEIAS 
//              Sticky arrows 

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] bool isHeatSeeker = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifeTimeAfterImpact = 2f;

        float damage = 0;
        Health target = null;

        private void Start()
        {
            // colocando o lookat aq ele não terá comportamento de heatSeeker
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            // colocando o lookat aqui ele terá uma comportamento de heatseeker 
            if (isHeatSeeker && !target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
            if (targetCollider == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCollider.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            // posso colocar sem especificar o target, assim ela acerta quem estiver na frente 
            if (other.GetComponent<Health>() != target) { return; }
            if (target.IsDead()) { return; }
            target.TakeDamage(damage);

            speed = 0;

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
                //Quaternion.LookRotation(target.transform.forward, target.transform.up)
            }

            foreach (var toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeTimeAfterImpact);
            // destruir depois de um tempo, ou se colidir com alguma coisa  
        }

    }

}