﻿using System;
using RPG.Combat;
using RPG.Movement;
using RPG.Resources;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        enum CursorType
        {
            None, 
            Combat,
            Movement,
            UI
        }

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType cursorType;
            public Texture2D texture;
            public Vector2 hotSpot;
        }

        [SerializeField] CursorMapping[] cursorMapping;

        private void Awake() 
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if(InteractWithUI()) return;
            if(health.IsDead()) 
            {
                SetCursor(CursorType.None);
                return; 
            }
            
            if (InteractWithCombat()) { return; }
            if (InteractWithMovement()) { return; }

            SetCursor(CursorType.None);
        }

        private bool InteractWithUI()
        {
            if(EventSystem.current.IsPointerOverGameObject())
            {
                SetCursor(CursorType.UI);
                return true;
            }
            return false; 
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
                SetCursor(CursorType.Combat);
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
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        // aqui eu posso colocar uma "use habilit" pq dai eu aperto o botão direito do mouse e a abilidade vai na direção que eu cliqeui, 
        // por exemplo o hit.point 

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotSpot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMapping)
            {
                if(mapping.cursorType == type)
                {
                    return mapping; 
                }
            }
            return cursorMapping[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
