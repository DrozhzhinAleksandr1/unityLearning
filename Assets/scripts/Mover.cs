using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    // [SerializeField] Transform targer;

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


    public void MoveTo(Vector3 destination)
    {
        GetComponent<NavMeshAgent>().destination = destination;
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }
}
