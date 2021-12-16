using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using RPG.Resources;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter; 
        Health health;
        TextMeshProUGUI percentageText;  

        private void Awake() 
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            percentageText = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            health = fighter.GetTarget();
            
            if(health == null)
            {
                percentageText.text = "N/A";
                return;     
            }
            percentageText.text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }

}