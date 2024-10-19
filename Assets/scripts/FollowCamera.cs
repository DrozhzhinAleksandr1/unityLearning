using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    [SerializeField] Transform targer;

    void LateUpdate()
    {
        transform.position = targer.position;
    }
}
