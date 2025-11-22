using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Attributes;
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
        int currentWaypointindex = 0;

        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float chaseDistance = 8f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float waypointDwellTime = 3f;
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;
        // Start is called before the first frame update
        void Awake()
        {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
        }

        void Start()
        {
            guardPosition = transform.position;
        }

        // // Update is called once per frame
        private void Update()
        {
            if (health.IsDead()) return;

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardPosition;
            if (patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                    // как только он оказываеться сдесь то он уже 
                    // текущий вейпоинт не считает текущим и поэтому не срабатывает много раз
                    // потому что CycleWaypoint сразу же меняеться на некст
                }
                nextPosition = GetCurrentWaypoint();
                // print($"{gameObject.name} next:{nextPosition}");
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                // тут пока не пройдет время ожидания на точке дальше не пойдет
                mover.StartMoveAction(nextPosition, patrolSpeedFraction);
            }

        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointindex);
        }

        private void CycleWaypoint()
        {
            currentWaypointindex = patrolPath.GetNextIndex(currentWaypointindex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {

            timeSinceLastSawPlayer = 0;
            fighter.Atack(player);
        }

        private bool InAttackRangeOfPlayer()
        {

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            // print($"{gameObject.name} should chase because distance is  below chaseDistance and equal :{distanceToPlayer}");
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