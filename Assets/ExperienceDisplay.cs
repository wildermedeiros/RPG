using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Resources
{
    public class ExperienceDisplay : MonoBehaviour
{
        Experience experience;
        TextMeshProUGUI percentageText;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            percentageText = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            percentageText.text = String.Format("{0:0}", experience.GetPoints());
        }
    }

}
