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

        NavMeshAgent navMeshAgent;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            // Debug.Log("Start is called once when the script is initialized.");
        }

        // Ray lastRay;

        // Update is called once per frame
        void Update()
        {
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

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }


        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
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