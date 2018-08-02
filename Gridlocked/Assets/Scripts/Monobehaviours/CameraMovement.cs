using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static float CameraHeight;
    public static float CameraWidth;

    public Transform targetTransform;
    private Vector3 currentTarget;

    public float smoothTime;

    private Vector3 velocity;

    public Transform right;
    public Transform left;

    public Transform up;
    public Transform down;

    private void Start()
    {
        CameraHeight = Camera.main.orthographicSize * 2;
        CameraWidth = CameraHeight * Camera.main.aspect;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {   
        currentTarget = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, currentTarget, ref velocity, smoothTime);      
    }
}