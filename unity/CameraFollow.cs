using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed;
    public Vector3 offset;
    public float lowest;
        Vector3 v = Vector3.zero;
    void FixedUpdate()
    {
        //Vector3 destPos = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed* Time.deltaTime);
        //if(destPos.y < lowest)
        //    destPos.y = lowest;
        //transform.position = destPos;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, target.position, ref v, smoothSpeed);
        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
 
    }
}