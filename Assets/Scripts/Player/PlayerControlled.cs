using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlled : MonoBehaviour
{
    public int runningSpeed = 7;// public variable which controls movement speed, choosing lower numbers to test with.

    private int speed;
    public int sprintSpeed = 10;

    public int jumpForce = 6;// public variable which controls how high the player can jump
    public CharacterController playercontroller;// controller component for the player avatar

    private Vector3 moveDirection;// allows for 3d movement, private variable which controls move direction
    public int gravityScale = 2;// controls how strong the gravity is

    public bool allowuserinput; //A bool variable which can enable or disable user input
    public static bool IsInputEnabled = true; //A bool variable which states that user input is enabled.

    public Transform model; //model of the player avatar, to rotate the model in a later script.

    //public float walkSpeed, possibly add later
    //public integer rotatespeed, possibly add later

    public GameObject thePlayer;

    public bool xaxis = true;
    public bool zaxis = false;

    void Start()
    {
        playercontroller = GetComponent<CharacterController>();// uses the CharacterController Unity component

        {
            if (!allowuserinput)
            {
                PlayerControlled.IsInputEnabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
 //Basic Left and Right movement code
        float HorizontalInput = (Input.GetAxisRaw("Horizontal")) * speed;

        if (PlayerControlled.IsInputEnabled)
        {
            if (xaxis)  // if x axis variable is true, moves direction on x axis
            moveDirection = new Vector3(HorizontalInput, moveDirection.y); //Sets the moveDirection variable.

            if (zaxis) // if z axis variable is true, moves direction on z axis
            moveDirection = new Vector3(0, moveDirection.y, HorizontalInput);

            if (playercontroller.isGrounded)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce; //makes jump force variable
                    Debug.Log("Player is Jumping");

                }
            }

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            playercontroller.Move(moveDirection * Time.deltaTime); //controls how direction, jump and gravity works for the player avatar
        }
  //Rotation code
        {
            if(HorizontalInput  != 0) //if input is detected, (value not detected as 0)
            {
                if (xaxis) // if x axis variable is true then only rotate on x axis, on left and right movement
                {
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(HorizontalInput, 0, 0));
                    model.rotation = newRotation;
                }
                if (zaxis) // if z axis variable is true then only rotate on z axis, on left and right movement
                {
                    Quaternion newRotation = Quaternion.LookRotation(new Vector3(0, 0, HorizontalInput));
                    model.rotation = newRotation;
                }
            }

        }                                                                                         
        // x axis co-ordinate is defined by horizontal input
        // if it's postive, rotates right, if it's negative, rotates left
        // the model rotation is equaled to the coordinate values created from line 67

 //Sprint code
        if (Input.GetKey(KeyCode.LeftShift)) // if left shift is pressed, speed variable will use sprintSpeed value
        {
            speed = sprintSpeed;
            Debug.Log("Sprint");
        }
        if (!Input.GetKey(KeyCode.LeftShift)) // if left shift is not being pressed, speed variable will use runningSpeed value
        {
            speed = runningSpeed;
        }


    }
// changes axis triggers
    void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.tag == "zaxischange"))
        {
            Debug.Log("AXIS CHANGE WOAH");
            zaxis = true;
            xaxis = false;
        }

        if ((other.gameObject.tag == "xaxischange"))
        {
            Debug.Log("AXIS CHANGE WOAH");
            zaxis = false;
            xaxis = true;
        }
    }

}