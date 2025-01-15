using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform targer;
        [SerializeField] float maxSpeed = 6f;


        Health health;
        NavMeshAgent navMeshAgent;

        private void Start()
        {
            health = GetComponent<Health>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            // Debug.Log("Start is called once when the script is initialized.");
        }

        // Ray lastRay;

        // Update is called once per frame
        void Update()
        {

            navMeshAgent.enabled = !health.IsDead();
            // // // // // first part learning
            // if (Input.GetMouseButtonDown(0))
            // {
            //     lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //     Debug.DrawRay(lastRay.origin, lastRay.direction * 100, Color.red, 5f);
            // }
            // GetComponent<NavMeshAgent>().destination = targer.position;
            // // // // // first part learning

            // if (Input.GetMouseButtonDown(0)) // клик
            // if (Input.GetMouseButton(0)) // когда зажата
            // {
            //     MoveToCursor();
            // }
            UpdateAnimator();
            // lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            // GetComponent<NavMeshAgent>().destination = targer.position;

        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }


        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            Stop();
        }
        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }

}