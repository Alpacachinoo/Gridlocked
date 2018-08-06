using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static float CameraHeight { get; private set; }
    public static float CameraWidth { get; private set; }

    public Transform targetTransform;
    private Vector3 currentTarget;

    public float smoothTime;

    private Vector3 velocity;

    public Transform ground;
    private float halfGroundLength;

    private void Start()
    {
        CameraHeight = Camera.main.orthographicSize * 2;
        CameraWidth = CameraHeight * Camera.main.aspect;

        halfGroundLength = ground.localScale.x * 10 / 2;
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {   
        currentTarget = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);

        CameraBoundaryCheck();

        transform.position = Vector3.SmoothDamp(transform.position, currentTarget, ref velocity, smoothTime);
    }

    private void CameraBoundaryCheck()
    {
        if ((ground.position.x - halfGroundLength) - (currentTarget.x - CameraWidth / 2) >= 0)
            currentTarget.x = ground.position.x + CameraWidth / 2 - halfGroundLength;

        if ((ground.position.x + halfGroundLength) - (currentTarget.x + CameraWidth / 2) <= 0)
            currentTarget.x = ground.position.x - CameraWidth / 2 + halfGroundLength;

        if ((ground.position.z - halfGroundLength) - (currentTarget.z - CameraHeight / 2) >= 0)
            currentTarget.z = ground.position.z + CameraHeight / 2 - halfGroundLength;

        if ((ground.position.z + halfGroundLength) - (currentTarget.z + CameraHeight / 2) <= 0)
            currentTarget.z = ground.position.z - CameraHeight / 2 + halfGroundLength;
    }
}