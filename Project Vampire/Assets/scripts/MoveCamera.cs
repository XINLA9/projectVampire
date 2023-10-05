using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float mouseWheelInput;
    public float speed = 10.0f;
    public float range = 20.0f;
    public float height_max;
    public float height_min;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        // move the camera left and right
        transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        if (transform.position.x <= -range)
        {
            transform.position = new Vector3(-range, transform.position.y, transform.position.z);
        }
        if (transform.position.x >= range)
        {
            transform.position = new Vector3(range, transform.position.y, transform.position.z);
        }
        // move the camera up and down
        transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        if (transform.position.z <= -range)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -range
                );
        }
        if (transform.position.z >= range)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, range);
        }
        // move the camera higher or lower
        transform.Translate(Vector3.forward * speed * 5 * mouseWheelInput * Time.deltaTime);
        if (transform.position.y <= height_min)
        {
            transform.position = new Vector3(transform.position.x, height_min, transform.position.z);
        }
        if (transform.position.y >= height_max)
        {
            transform.position = new Vector3(transform.position.x, height_max, transform.position.z);
        }
    }
}
