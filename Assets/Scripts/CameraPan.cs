using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float RecenterTime;
    public float CameraHorizontalOffSet;
    public float CameraVerticalOffset;
    private Transform playerTransform;
    private Rigidbody2D playerRigidbody2D;
    public Vector3 currentCameraVelocity;

    void Awake()
    {
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        playerRigidbody2D = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        transform.position = playerTransform.position;
        currentCameraVelocity = Vector3.zero;
    }

    void LateUpdate()
    {
        Vector2 playerDirection = playerRigidbody2D.velocity.normalized;
        Vector3 playerVelocity = Vector3.zero;
        Vector3 offSetPosition = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
        offSetPosition.x += CameraHorizontalOffSet * playerDirection.x;
        offSetPosition.y += CameraVerticalOffset * playerDirection.y;

        transform.position = Vector3.SmoothDamp(transform.position, offSetPosition, ref currentCameraVelocity,RecenterTime);
    }
}
