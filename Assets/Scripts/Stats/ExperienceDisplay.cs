using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
{
        Experience experience;
        TextMeshProUGUI experienceAmountText;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            experienceAmountText = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            experienceAmountText.text = String.Format("{0:0}", experience.GetPoints());
        }
    }

}
