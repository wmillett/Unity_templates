using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    Rigidbody rb;

    public float moveSpeed = 5f;   // Realistic speed
    public float jumpForce = 5f;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the GameObject.");
        }
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Only allow jumping if grounded and Jump key is pressed
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isGrounded = false; // Temporarily disable jumping until grounded again
        }

        float rotationY = Input.GetAxis("Mouse X") * moveSpeed;
        if (rotationY != 0)
        {
            transform.Rotate(0, rotationY * Time.deltaTime, 0);
        }

        Vector3 moveDirection = new Vector3(moveX, 0, moveZ) * moveSpeed;
        Vector3 newPosition = rb.position + transform.TransformDirection(moveDirection) * Time.deltaTime;

        rb.MovePosition(newPosition);
    }

    // Called when the object touches something
    private void OnCollisionEnter(Collision collision)
    {
        // Check if we hit something tagged "Ground"
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Called when the object stops touching something
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}




/*
using System.Collections.Generic;
using UnityEngine;

public class controls : MonoBehaviour
{
    [SerializeField] Transform plane;
    [SerializeField] float rotateXSpeed;  // renamed from pitchSpeed
    [SerializeField] float rotateZSpeed;  // renamed from yawSpeed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rotateX = Input.GetAxisRaw("Vertical") * rotateXSpeed * Time.deltaTime;    // Up/Down arrows
        float rotateZ = -Input.GetAxisRaw("Horizontal") * rotateZSpeed * Time.deltaTime;  // Left/Right arrows

        plane.Rotate(rotateX, 0, rotateZ);  // Changed to use X and Z rotation
    }
}
*/