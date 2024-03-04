using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{

    bool isJumping = false;

    float inputHorizontal;
    float inputVertical;
    bool inputJump;

    public float jumpForce = 10f;
    public float jumpTime = 1.5f;
    public float velocity = 5f;
    public float gravity = 9.8f;

    PlayerState playerState;

    Animator animator;
    CharacterController cc;

    float jumpElapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerState = PlayerState.Grounded;
    }

    // Update is called once per frame
    void Update()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputJump = Input.GetKeyDown(KeyCode.Space);

        if ( inputJump && cc.isGrounded )
        {
            isJumping = true;
            animator.SetTrigger("jump");
            animator.SetBool("air", true);
            //playerState = PlayerState.Jumping;
        }
        else
        {
            animator.SetBool("run", inputHorizontal != 0 || inputVertical != 0);
        }

    }

    private void FixedUpdate()
    {

        float directionX = inputHorizontal * velocity * Time.deltaTime;
        float directionZ = inputVertical * velocity * Time.deltaTime;

        float directionY = 0;
        if ( !isJumping && cc.isGrounded )
        {
            animator.SetBool("air", false);
            jumpElapsedTime = 0;
        }

        if (isJumping)
        {
            jumpElapsedTime += Time.deltaTime;

            directionY = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime);
            directionY *= Time.deltaTime;

            //directionY = jumpForce;

            if(jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
            }
        }

        directionY -= gravity * Time.deltaTime;

        // ################# Rotação do personagem
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        forward = forward * directionZ;
        right = right * directionX;

        if (directionX != 0 || directionZ != 0)
        {
            float angle = Mathf.Atan2(forward.x + right.x, forward.z + right.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0, angle, 0);
            //transform.rotation = rotacao;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        Vector3 verticalDirection = Vector3.up * directionY;
        Vector3 horizontalDirection = forward + right;

        Vector3 moviment = verticalDirection + horizontalDirection;
        cc.Move( moviment );
    }

}
