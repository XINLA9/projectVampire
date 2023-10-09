using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{
    private float cameraSpeed = 20.0f; // Adjust the camera movement speed.
    private float scrollSpeed = 20.0f; // Adjust the scroll speed.
    private float maxX = 40.0f;
    private float maxZ = 25.0f;
    private float minY = 10.0f;
    private float maxY = 40.0f;

    void Update()
    {
        // Get the mouse position in screen coordinates (pixels).
        Vector3 mousePosition = Input.mousePosition;

        // Define a threshold for the screen edges (in pixels) where camera movement will start.
        int edgeThreshold = 20; // Adjust this value as needed.

        // Get the screen dimensions in pixels.
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        // Initialize movement vector.
        Vector3 movement = Vector3.zero;

        // Check if the mouse is near the left edge.
        if (mousePosition.x < edgeThreshold && transform.position.x < maxX)
        {
            movement += Vector3.left;
        }
        // Check if the mouse is near the right edge.
        else if (mousePosition.x > screenWidth - edgeThreshold && transform.position.x > -maxX)
        {
            movement += Vector3.right;
        }
        // Check if the mouse is near the bottom edge.
        if (mousePosition.y < edgeThreshold && transform.position.z < maxZ)
        {
            movement += Vector3.down;
        }
        // Check if the mouse is near the top edge.
        else if (mousePosition.y > screenHeight - edgeThreshold && transform.position.z > -maxZ)
        {
            movement += Vector3.up;
        }

        // Adjust the camera's Y position using the mouse wheel.
        float scrollInput = Input.mouseScrollDelta.y;
        Vector3 scrollMovement = Vector3.forward * scrollInput * scrollSpeed * Time.deltaTime;

        // Normalize the movement vector to maintain consistent speed.
        if (movement != Vector3.zero)
        {
            movement.Normalize();
            movement *= cameraSpeed * Time.deltaTime;
        }

        // Combine the movement and scroll movement.
        Vector3 finalMovement = movement + scrollMovement;

        // Translate the camera.
        transform.Translate(finalMovement);

        // Ensure the camera stays within the defined boundaries and minY.
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -maxX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -maxZ, maxZ);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }
}
