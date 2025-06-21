using System;
using RPG.Attributes;
using TMPro;
using UnityEngine;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {

        Fighter fighter;
        TextMeshProUGUI text;
        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            text = GetComponent<TextMeshProUGUI>();
        }

        void Update()
        {
            Health targetHealth = fighter.GetTarget();
            if (targetHealth == null)
            {
                text.text = "Empty";
            }
            else
            {
                // text.text = String.Format("{0:0}%", targetHealth.GetPercantage());
                text.text = String.Format("{0:0}/{1:0}", targetHealth.GetHealthPoints(), targetHealth.GetMaxHealthPoints());
            }
        }

    }
}