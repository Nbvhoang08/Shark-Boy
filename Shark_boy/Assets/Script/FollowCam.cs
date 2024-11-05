using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public Transform target; // Đối tượng mà camera sẽ theo dõi
    public float smoothSpeed = 0.125f; // Tốc độ làm mượt
    public Vector3 offset; // Độ lệch giữa camera và đối tượng
    public float minX; // Tọa độ X tối thiểu
    public float maxX;
    void FixedUpdate()
    {
        if (target != null)
        {
            float desiredX = target.position.x + offset.x; 
            desiredX = Mathf.Clamp(desiredX, minX, maxX); // Giới hạn giá trị X
            Vector3 desiredPosition = new Vector3(desiredX, transform.position.y, transform.position.z); 
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); 
            transform.position = smoothedPosition;
        }
    }
}
