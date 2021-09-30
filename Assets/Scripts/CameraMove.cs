using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform targetObject;
    public Vector3 CameraOffset;
    public float smoothFactor = 0.5f;
    public bool lookAtTarget = false;
    void Start()
    {
        CameraOffset = transform.position - targetObject.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPostion = targetObject.transform.position + CameraOffset;
        //transform.position = Vector3.Slerp(transform.position, newPostion, smoothFactor);
        transform.position = Vector3.Lerp(transform.position, newPostion, smoothFactor);


        if (lookAtTarget)
        {
            transform.LookAt(targetObject);
        }
    }
}
