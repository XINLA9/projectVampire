using UnityEngine;

public class OldMoveCamera : MonoBehaviour
{
    private float moveSpeed = 20.0f;

    void Update()
    {
        // Get input from arrow keys or WASD keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement vector
        Vector3 moveDirection = new Vector3(-horizontalInput, 0.0f, -verticalInput); // Lock Y-axis to 0.0f

        // Normalize the vector to maintain constant speed when moving diagonally
        moveDirection.Normalize();

        // Move the camera based on input, preserving Y position
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}
