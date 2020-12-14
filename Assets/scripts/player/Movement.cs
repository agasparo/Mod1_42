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
    bool isAnimmated = false;
    public Camera playerView;


    void Update() {

    	isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isSwimming = Physics.CheckSphere(waterCheck.position, waterDistance, waterMask);

        if (isSwimming)
        {
            if (!isAnimmated && !isGrounded)
            {
                playerView.transform.position = new Vector3(playerView.transform.position.x, playerView.transform.position.y - 3.30f, playerView.transform.position.z);
                isAnimmated = true;
                speed = 5f;
            }
        } else {
            
            if (isAnimmated && isGrounded)
            {
                playerView.transform.position = new Vector3(playerView.transform.position.x, playerView.transform.position.y + 3.30f, playerView.transform.position.z);
                isAnimmated = false;
                speed = 20f;
            }
        }

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
