using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

	public CharacterController controller;
	public float speed = 20f;
	public float gravity = -50.81f;
	public float jumpHeight = 3f;
    public static int teleState = 1;

    public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
    public GameObject Water;

    Vector3 velocity;
	bool isGrounded;
    bool isSwimming = false;

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    void Update() {

    	isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if ((isGrounded || isSwimming) && velocity.y < 0)
    		velocity.y = -2f;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.R))
        {
            controller.enabled = false;
            if (teleState == 0)
                controller.transform.position = new Vector3(50f, 252f, 50f);
            else
                controller.transform.position = new Vector3(50f, 150f, 50f);
            controller.enabled = true;
            teleState = (teleState - 1) * -1;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (!isSwimming)
        {
            if (Input.GetButtonDown("Jump") && isGrounded) {

                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }
}
