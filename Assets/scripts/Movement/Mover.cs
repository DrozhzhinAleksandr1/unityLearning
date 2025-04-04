using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
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

        // public object CaptureState()
        // {
        //     return new SerializableVector3(transform.position);
        // }

        // public void RestoreState(object state)
        // {
        //     SerializableVector3 position = (SerializableVector3)state;
        //     GetComponent<NavMeshAgent>().enabled = false;
        //     transform.position = position.ToVector();
        //     GetComponent<NavMeshAgent>().enabled = true;
        // }

        //save with struct

        [System.Serializable]
        struct MoverSaveData
        {
            public SerializableVector3 position;
            public SerializableVector3 rotation;
        }

        public object CaptureState()
        {
            MoverSaveData data = new MoverSaveData();
            data.position = new SerializableVector3(transform.position);
            data.rotation = new SerializableVector3(transform.eulerAngles);
            return data;
        }

        public void RestoreState(object state)
        {
            MoverSaveData data = (MoverSaveData)state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = data.position.ToVector();
            transform.eulerAngles = data.rotation.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }

        // save use dictionary

        // public object CaptureState()
        // {
        //     Dictionary<string, object> data = new Dictionary<string, object>();
        //     data["position"] = new SerializableVector3(transform.position);
        //     data["rotation"] = new SerializableVector3(transform.eulerAngles);
        //     return data;
        // }

        // public void RestoreState(object state)
        // {
        //     Dictionary<string, object> data = (Dictionary<string, object>)state;
        //     GetComponent<NavMeshAgent>().enabled = false;
        //     transform.position = ((SerializableVector3)data["position"]).ToVector();
        //     transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
        //     GetComponent<NavMeshAgent>().enabled = true;
        // }
    }

}