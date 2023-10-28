using UnityEngine;

public class CameraFollowMouse : MonoBehaviour
{
    private float cameraSpeed = 20.0f; // Adjust the camera movement speed.
    private float scrollSpeed = 40.0f; // Adjust the scroll speed.
    private float maxX = 30.0f;
    private float maxZ = 25.0f;
    private float minY = 10.0f;
    private float maxY = 40.0f;

    void Update()
    {
        // Get the mouse position in screen coordinates (pixels).
        Vector3 mousePosition = Input.mousePosition;

        // Define a threshold for the screen edges (in pixels) where camera movement will start.
        int edgeThreshold = 10; // Adjust this value as needed.

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
        if (mousePosition.y < edgeThreshold && transform.position.z < maxZ + 15)
        {
            movement += new Vector3(0.0f, -2.0f, -1.15f); // Use Vector3.back instead of Vector3.down for the Z-axis.
        }
        // Check if the mouse is near the top edge.
        else if (mousePosition.y > screenHeight - edgeThreshold && transform.position.z > -maxZ + 15)
        {
            movement += new Vector3(0.0f, 2.0f, 1.15f); // Use Vector3.forward instead of Vector3.up for the Z-axis.
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

        // Combine the movement and scroll movement, but set Y component to 0.
        Vector3 finalMovement = new Vector3(movement.x, movement.y, movement.z) + scrollMovement;

        // Translate the camera.
        transform.Translate(finalMovement);

        // Ensure the camera stays within the defined boundaries and minY.
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -maxX, maxX);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, -maxZ + 15, maxZ + 15);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minY, maxY);
        transform.position = clampedPosition;
    }
}
