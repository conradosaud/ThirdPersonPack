﻿using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.TextCore.Text;

/*
    You are lost? Read the Tutorial.pdf file from this pack. There is a lot of information there. 
*/


/// <summary>
/// Main script for third-person movement of the character in the game.
/// Make sure that the object that will receive this script (the player) 
/// has the Player tag and the Character Controller component.
/// </summary>
public class CommentedThirdPersonController : MonoBehaviour
{

    [Tooltip("Speed ​​at which the character moves. It is not affected by gravity or jumping.")]
    public float velocity = 5f;
    [Tooltip("This value is added to the speed value while the character is sprinting.")]
    public float sprintAdittion = 3.5f;
    [Tooltip("The higher the value, the higher the character will jump.")]
    public float jumpForce = 18f;
    [Tooltip("Stay in the air. The higher the value, the longer the character floats before falling.")]
    public float jumpTime = 0.85f;

    [Space]
    [Tooltip("Force that pulls the player down. Changing this value causes all movement, jumping and falling to be changed as well.")]
    public float gravity = 9.8f;

    // Checks the character's current state
    bool isJumping = false;
    bool isSprinting = false;
    bool isCrouching = false;

    // Identifiable game keys input for the player
    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputCrouch;
    bool inputSprint;

    // Get control of the character's animations
    Animator animator;
    // Gets the character's collision and movement controller component
    CharacterController cc;

    // Variable controlling the time the player spent in the air. Explained further below.
    float jumpElapsedTime = 0;

    

    void Start()
    {
        // Starts any of the above variables when starting the game
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }


    // Update is only being used here to identify keys and trigger animations
    void Update()
    {

        // Check which input is being pressed
        // Read the Tutorial.pdf for a detailed explanation
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputJump = Input.GetAxis("Jump") == 1f;
        inputSprint = Input.GetAxis("Fire3") == 1f;
        // Unfortunately GetAxis does not work with GetKeyDown, so inputs must be taken individually
        inputCrouch = Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.JoystickButton1);

        // Check if you pressed the crouch input key and change the player's state.
        // Note: It is possible to make changes to keep player crouched only while the key is pressed
        if ( inputCrouch == true )
        {
            isCrouching = !isCrouching; // Use the ! in a boolean is a way to toggle it!
        }

        // isGrounded is a Character Controller property that informs whether the player is touching the ground. It's very easy to use!
        if ( cc.isGrounded )
        {
            // If the crouched state is true, it runs its animation, otherwise, not.
            // Note: The crouch animation does not shrink the character's collider
            if ( isCrouching == true )
            {
                animator.SetBool( "crouch", true );
            }
            else
            {
                animator.SetBool( "crouch", false );
            }

            // Check the player's speed and whether it is high enough to trigger the running animation
            float minimumSpeed = 0.9f; // You can test with other values
            if ( cc.velocity.magnitude > minimumSpeed) 
            {
                animator.SetBool( "run", true );
            }
            else
            {
                animator.SetBool( "run", false );
            }

            // Same logic as the race, but adding the sprint input condition
            if ( cc.velocity.magnitude > 0.9f && inputSprint )
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }
            
            // After going through the above condition, we already have the answer to whether it is running or not within the variable
            animator.SetBool("sprint", isSprinting );

        }

        // Air/jumping animation if is or not in the ground
        if( cc.isGrounded == true )
        {
            animator.SetBool( "air", false );
        }
        else
        {
            animator.SetBool( "air", true );
        }

        // Check if input jump is pressed and if player is in the ground
        if ( inputJump && cc.isGrounded )
        {
            isJumping = true;
            isCrouching = false;
        }

        // It's at the end of the code. Leave it for later.
        HeadHittingDetect();

    }


    // With the inputs and animations defined, FixedUpdate is responsible for applying movements and actions to the player
    private void FixedUpdate()
    {

        // This block checks if the player is sprinting, because if he is, it will add a speed boost to his movement
        float velocityAdittion = 0;
        if ( isSprinting )
        {
            velocityAdittion = sprintAdittion;
        }

        // Let's use the player's inputs to tell us if he moved to either side
        // And if it moved, let's make it faster by multiplying by speed and reducing by Time.DeltaTime
        // More explanations in Tutorial.pdf
        float directionX = inputHorizontal * (velocity + velocityAdittion) * Time.deltaTime;
        float directionZ = inputVertical * (velocity + velocityAdittion) * Time.deltaTime;
        // The Y position is the upward movement, and as our character doesn't fly, he just stays at zero :)
        float directionY = 0;

        // Let's check if the player jumped
        if ( isJumping )
        {
            // We are using a Unity function to make the jump more "subtle" and apply the natural feeling of inertia of the movement
            // Learn more here: https://docs.unity3d.com/ScriptReference/Mathf.SmoothStep.html
            // You can test this to see how strange it would sound: directionY = jumpForce * Time.deltaTime;
            directionY = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime) * Time.deltaTime;

            // Increases the time that has passed since the player started the jump
            jumpElapsedTime += Time.deltaTime;
            // But if the elapsed time is longer than expected jump time, it's time to start falling
            if ( jumpElapsedTime >= jumpTime )
            {
                isJumping = false; // It's not jumping anymore
                jumpElapsedTime = 0; // We reset the time so that the player can jump again later
            }
        }

        // After calculating the movement and jump, it's time to apply gravity
        // It needs to be negative so that the game always throws the player down
        directionY = directionY - gravity * Time.deltaTime;

        /*
            All of the content above is about character movement and jumping, and it all works well.
            From here on down, we'll apply rotation to player faces in direction of the pressed input

            It may seem confusing at first, and don't worry if you don't understand it at first.
            The truth is that over time you will continue without it and life will go on normally XD
        */

        // First, we need to locate which side is the player's front and right side.
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // We will not rotate the Y axis, as this would cause the player to hit the ground
        forward.y = 0;
        right.y = 0;

        // Normalization makes the calculation uniform
        // If you don't put this in, your player will move faster if he moves diagonally
        forward.Normalize();
        right.Normalize();

        // Let's relate the front with the Z direction (3d depth) and right with X (lateral movement)
        forward = forward * directionZ;
        right = right * directionX;

        // This condition is used to check whether the player is moving.
        // If you don't add it, the player will always look at the origin of the world after releasing a key. It's even funny, try it.
        if ( directionX != 0 || directionZ != 0 )
        {
            // This function returns the angle in radians whose tangent is the quotient of the two given arguments
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            // Applies the player's previously calculated rotation.
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            // Slerp makes movement smoother. If you want, you can test like this: transform.rotation = rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        /*
            This is the end of the character's rotation. The code below just applies everything to the character's actual movement 
        */

        // Vertical movement is gravity or jumping. All of this has already been previously defined in the directionY
        Vector3 verticalDirection = Vector3.up * directionY;
        // Here is the conversion of forward and right movement to actual positioning in the world based on X and Z (Y is above)
        Vector3 horizontalDirection = forward + right;

        // Finally, we apply this movement to the character controller, which will move our player
        Vector3 moviment = verticalDirection + horizontalDirection;
        cc.Move( moviment );
    }


    //This function makes the character end his movement if he hits his head on something.
    // If you remove this, you will notice that your character continues to "float" in the air if he hits his head against a wall
    void HeadHittingDetect()
    {
        float headHitDistance = 1.1f;
        Vector3 ccCenter = transform.TransformPoint(cc.center);
        float hitCalc = cc.height / 2f * headHitDistance;

        // Uncomment this line to see the Ray drawed in your characters head
        // Debug.DrawRay(ccCenter, Vector3.up * headHeight, Color.red);

        if (Physics.Raycast(ccCenter, Vector3.up, hitCalc))
        {
            jumpElapsedTime = 0;
            isJumping = false;
        }
    }

}
