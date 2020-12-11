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

    public Transform waterCheck;
    public float waterDistance = 0.4f;
    public LayerMask waterMask;

    Vector3 velocity;
	bool isGrounded;
    bool isSwimming;

    void Update() {

    	isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isSwimming = Physics.CheckSphere(waterCheck.position, waterDistance, waterMask);


        if ((isGrounded || isSwimming) && velocity.y < 0)
    		velocity.y = -2f;
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded && !isSwimming) {

        	velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

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

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
