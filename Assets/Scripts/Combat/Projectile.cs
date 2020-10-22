using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] bool isHeatSeeker = false;

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
        transform.Translate(Vector3.forward * speed *Time.deltaTime);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
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
        if (other.GetComponent<Health>() != target) { return; }
        
        if(target.IsDead()) { return; }

        target.TakeDamage(damage);
        Destroy(gameObject);
        // destruir depois de um tempo, ou se colidir com alguma coisa  
        // posso colocar sem especificar o target, assim ela acerta quem estiver na frente 
    }

}
