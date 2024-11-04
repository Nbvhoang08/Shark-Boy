using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target;  // Đối tượng mà camera sẽ theo dõi
    public float smoothSpeed = 0.125f;  // Tốc độ làm mượt
    public Vector3 offset;  // Độ lệch giữa camera và đối tượng

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
