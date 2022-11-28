using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationStateController : MonoBehaviour
{
    Animator animator; // private variable, animator component for the player avatar


    void Start()
    {
        animator = GetComponent<Animator>(); // uses the Animator Unity component
    }

    
    void Update()
    {
        bool isRunning = animator.GetBool("isRunning");
        bool isJumping = animator.GetBool("isJumping");
        bool isSprinting = animator.GetBool("isSprinting");

        bool movementPressed = Input.GetKey("d") | Input.GetKey("a");
        bool sprintPressed = Input.GetKey(KeyCode.LeftShift);
        bool jumpPressed = Input.GetButtonDown("Jump");


        // Foward Run (pressing "d"/"a", right key)
        if (!isRunning && movementPressed && !sprintPressed)               // if the running animation is set to false (not playing) and the "d"/"a" key is being pressed and "leftshift" is not pressed
        {                                                                  // then
            animator.SetBool("isRunning", true);                           // running animation is set to true (playing)

        }

        if (isRunning && !movementPressed)                                 //if the running animation is set to true (playing) and the "d"/"a" key is not pressed
        {                                                                  //then
            animator.SetBool("isRunning", false);                          // running animation is set to false (not playing)
        }
                                                

        // Forwards Sprint (pressing left shift)
        if (!isSprinting && sprintPressed && movementPressed)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isSprinting", true);
        }

        if (isSprinting && !sprintPressed)
        {
            animator.SetBool("isSprinting", false);
        }

        //Jumping animations (Space pressed)
        {
            if (!isJumping && jumpPressed)
            {
                animator.SetBool("isJumping", true);
            }

            if (isJumping && !jumpPressed)
            {
                animator.SetBool("isJumping", false);
            }
        }
    }
}
