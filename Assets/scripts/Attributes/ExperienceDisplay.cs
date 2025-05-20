using System;
using TMPro;
using UnityEngine;


namespace RPG.Attributes
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience;
        TextMeshProUGUI text;
        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            text = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            // text.text = String.Format("{0:0.0}%", health.GetPercantage()); // if wanna see 79.8%
            text.text = String.Format("{0:0}", experience.GetPoints()); // show only full number
        }

    }
}