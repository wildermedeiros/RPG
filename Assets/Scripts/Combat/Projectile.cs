using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(GetAimLocation());
        transform.Translate(Vector3.forward * speed *Time.deltaTime);
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCollider = target.GetComponent<CapsuleCollider>();
        if (targetCollider == null)
        {
            return target.position;
        }
        return target.position + Vector3.up * targetCollider.height / 2;  
    }
}
