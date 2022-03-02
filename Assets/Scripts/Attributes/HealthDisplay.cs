using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI percentageText;  

        private void Awake() 
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            percentageText = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            percentageText.text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }

}