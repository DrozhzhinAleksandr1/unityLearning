using System;
using RPG.Stats;
using TMPro;
using UnityEngine;


namespace RPG.Attributes
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats;
        TextMeshProUGUI text;
        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            text = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            // text.text = String.Format("{0:0.0}%", health.GetPercantage()); // if wanna see 79.8%
            text.text = String.Format("{0:0}", baseStats.GetLevel()); // show only full number
        }

    }
}