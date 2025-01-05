using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        Fighter fighter;
        Mover mover;
        Health health;
        GameObject player;

        Vector3 guardPosition;
        [SerializeField] float chaseDistance = 8f;
        // Start is called before the first frame update
        void Start()
        {
            guardPosition = transform.position;
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
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
                // fighter.Cancel();
                mover.StartMoveAction(guardPosition);
            }
        }

        private bool InAttackRangeOfPlayer()
        {

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            print($"{gameObject.name} should chase because distance is  below chaseDistance and equal :{distanceToPlayer}");
            return distanceToPlayer < chaseDistance;
        }


        /// ////////////////////////////////////////////////////////////////////

        // Called by Unity

        // private void OnDrawGizmos()
        // {

        // }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        /// ////////////////////////////////////////////////////////////////////
    }
}