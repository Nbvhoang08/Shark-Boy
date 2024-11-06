using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    public Transform pointA; // Điểm đầu tiên
    public Transform pointB; // Điểm thứ hai
    public float speed = 2.0f; // Tốc độ di chuyển

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = pointB.position;
        transform.position = pointA.position; // Bắt đầu di chuyển tới điểm B
    }

    void Update()
    {
        // Di chuyển đối tượng tới vị trí mục tiêu
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Kiểm tra nếu đối tượng đã tới vị trí mục tiêu
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Đổi vị trí mục tiêu
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }
}
