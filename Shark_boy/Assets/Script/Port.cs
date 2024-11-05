using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
   [Header("Portal Settings")]
    [SerializeField] private float rotationInterval = 2f;    // Thời gian giữa mỗi lần xoay (giây)
    [SerializeField] private float rotationAngle = 45f;      // Góc xoay mỗi lần (độ)
    [SerializeField] private float rotationDuration = 0.5f;  // Thời gian để hoàn thành một lần xoay (giây)
    [SerializeField] private float launchForce = 10f;        // Lực bắn player

    [SerializeField] private float currentAngle = 0f;           // Góc hiện tại
    private float targetAngle = 0f;            // Góc đích
    [SerializeField] private float rotationTimer = 0f;          // Timer cho việc xoay
    [SerializeField] private float intervalTimer = 0f;          // Timer cho khoảng thời gian giữa các lần xoay
    private bool isRotating = false;           // Đang trong quá trình xoay?
    
    private GameObject playerInPortal;
    private Rigidbody2D playerRb;

   
    private void Update()
    {
        if (!isRotating)
        {
            // Đếm thời gian cho đến lần xoay tiếp theo
            intervalTimer += Time.deltaTime;
            if (intervalTimer >= rotationInterval)
            {
                // Bắt đầu một lần xoay mới
                StartNewRotation();
            }
            CheckLaunchPlayer();
        }
        else
        {
            // Đang trong quá trình xoay
            UpdateRotation();
        }

        // Kiểm tra input để bắn player
      
    }

    private void StartNewRotation()
    {
        isRotating = true;
        intervalTimer = 0f;
        rotationTimer = 0f;
        targetAngle = currentAngle + rotationAngle;
        
        // Giữ targetAngle trong khoảng 0-360
        if (targetAngle >= 360f)
        {
            targetAngle -= 360f;
        }
    }

    private void UpdateRotation()
    {
        rotationTimer += Time.deltaTime;
        float t = rotationTimer / rotationDuration;

        if (t >= 1f)
        {
            // Kết thúc xoay
            currentAngle = targetAngle;
            isRotating = false;
        }
        else
        {
            // Lerp để xoay mượt
            float startAngle = targetAngle - rotationAngle;
            currentAngle = Mathf.Lerp(startAngle, targetAngle, t);
        }

        // Áp dụng rotation mới
        transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);
    }

    private void CheckLaunchPlayer()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) )&& playerInPortal != null && playerRb != null)
        {
            LaunchPlayer();
        }
    }

    private void LaunchPlayer()
    {
        float angleInRadians = currentAngle * Mathf.Deg2Rad;
        Vector2 launchDirection = new Vector2(
            Mathf.Cos(angleInRadians),
            Mathf.Sin(angleInRadians)
        );
        SoundManager.Instance.PlayVFXSound(0);
        playerRb.velocity = launchDirection * launchForce;
     
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInPortal = other.gameObject;
            playerRb = other.GetComponent<Rigidbody2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInPortal = null;
            playerRb = null;
        }
    }

    private void OnDrawGizmos()
    {
        // Vẽ hướng của portal
        Gizmos.color = Color.red;
        Vector3 direction = Quaternion.Euler(0, 0, currentAngle) * Vector3.right;
        Gizmos.DrawRay(transform.position, direction * 2f);
        
        // Vẽ vùng trigger
        Gizmos.color = Color.yellow;
        if (TryGetComponent<CircleCollider2D>(out CircleCollider2D collider))
        {
            Gizmos.DrawWireSphere(transform.position, collider.radius);
        }
    }
}
