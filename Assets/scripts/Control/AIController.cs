using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        Fighter fighter;
        Health health;
        GameObject player;
        [SerializeField] float chaseDistance = 8f;
        // Start is called before the first frame update
        void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
        }

        // // Update is called once per frame
        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                fighter.Atack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            print($"{gameObject.name} should chase because distance is  below chaseDistance and equal :{distanceToPlayer}");
            return distanceToPlayer < chaseDistance;
        }
    }
}