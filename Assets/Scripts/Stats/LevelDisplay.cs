using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        TextMeshProUGUI currentLevelText;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            currentLevelText = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            currentLevelText.text = String.Format("{0:0}", baseStats.GetLevel());
        }
    }
}

