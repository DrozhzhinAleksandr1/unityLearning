using System;
using TMPro;
using UnityEngine;


namespace RPG.Attributes
{
    public class NewBehaviourScript : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI text;
        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            text = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            // text.text = String.Format("{0:0.0}%", health.GetPercantage()); // if wanna see 79.8%
            // text.text = String.Format("{0:0}%", health.GetPercantage()); // show only full number
            text.text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints()); // show only full number
        }

    }
}