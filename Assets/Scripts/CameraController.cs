using UnityEngine;

public class CameraController : MonoBehaviour
{
    // References
    public Transform tankBody;        // The main body of the tank
    public Transform turret;          // The turret of the tank
    public Transform cameraTransform; // The camera's transform

    // Camera settings
    public Vector3 offset = new Vector3(0, 5, -10); // Offset from the tank body
    public float followSpeed = 5f;    // Speed at which the camera follows the tank
    public float rotationSpeed = 5f;  // Speed at which the camera rotates horizontally
    public float verticalAngle = 20f; // Fixed vertical angle in degrees

    // Optional: Enable vertical rotation control
    public bool controlVerticalRotation = false;
    public float verticalRotationSpeed = 50f;
    private float currentVerticalAngle;

    private void Start()
    {
        // Initialize the current vertical angle
        currentVerticalAngle = verticalAngle;
    }

    private void LateUpdate()
    {
        HandleCameraPosition();
        HandleCameraRotation();
    }

    private void HandleCameraPosition()
    {
        // Create a rotation that only considers the turret's horizontal angle
        Quaternion horizontalRotation = Quaternion.Euler(0, turret.eulerAngles.y, 0);

        // Calculate the desired position based on the tank's position and horizontal rotation
        Vector3 desiredPosition = tankBody.position + horizontalRotation * offset;

        // Smoothly move the camera towards the desired position
        cameraTransform.position = Vector3.Lerp(
            cameraTransform.position,
            desiredPosition,
            followSpeed * Time.deltaTime);
    }

    private void HandleCameraRotation()
    {
        // Handle vertical rotation control
        if (controlVerticalRotation)
        {
            // Get vertical input (invert if necessary)
            float verticalInput = -Input.GetAxis("Mouse Y") * verticalRotationSpeed * Time.deltaTime;
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle + verticalInput, -10f, 80f); // Adjust limits as needed
        }

        // Create a rotation with the turret's horizontal angle and the defined vertical angle
        Quaternion desiredRotation = Quaternion.Euler(
            currentVerticalAngle,
            turret.eulerAngles.y,
            0);

        // Smoothly rotate the camera towards the desired rotation
        cameraTransform.rotation = Quaternion.Slerp(
            cameraTransform.rotation,
            desiredRotation,
            rotationSpeed * Time.deltaTime);
    }
}
