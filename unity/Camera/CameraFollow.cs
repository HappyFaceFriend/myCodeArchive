using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] smoothSpeed;
    [SerializeField] Vector3 offset;
    Vector3 v = Vector3.zero;
    void FixedUpdate()
    {
        Vector3 newPos = Vector3.SmoothDamp(transform.position, target.position, ref v, smoothSpeed);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
