using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start() 
        {
            health = GetComponent<Health>();    
        }

        void Update()
        {
            if(health.IsDead()) { return; }
            
            if (InteractWithCombat()) { return; }
            if (InteractWithMovement()) { return; }
            //print("nothing to do.");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) { continue; }

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) { continue; }
                
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        // Como fazer a movimentação para ela não travar em objetos que estiverem na frente do terreno
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        // aqui eu posso colocar uma "use habilit" pq dai eu aperto o botão direito do mouse e a abilidade vai na direção que eu cliqeui, 
        // por exemplo o hit.point 

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
