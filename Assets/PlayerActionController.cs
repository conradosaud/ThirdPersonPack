using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{

    bool isJumping = false;
    bool isSprinting = false;
    bool isAttacking = false;

    float inputHorizontal;
    float inputVertical;
    bool inputJump;
    bool inputAttack;
    bool inputSprint;

    public float jumpForce = 10f;
    public float jumpTime = 1.5f;
    public float velocity = 5f;
    public float sprintAdittion = 2f;
    public float gravity = 9.8f;

    public float headHitDetect = 0.5f;

    Animator animator;
    CharacterController cc;

    float jumpElapsedTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Input.GetAxis("Vertical");
        inputJump = Input.GetAxis("Jump") == 1f;
        inputAttack = Input.GetAxis("Fire1") == 1f;
        inputSprint = Input.GetAxis("Fire3") == 1f;

        if(cc.isGrounded)
        {
            animator.SetBool("run", cc.velocity.magnitude > 0.9f);

            isSprinting = cc.velocity.magnitude > 0.9f && inputSprint;
            animator.SetBool("sprint", isSprinting );
        }


        animator.SetBool("air", cc.isGrounded == false );
        
        if( inputJump && cc.isGrounded)
        {
            isJumping = true;
        }

        if( inputAttack && ! isAttacking )
        {
            isAttacking = true;
            animator.SetBool("attack", true);
        }

        HeadHittingDetect();

    }

    private void FixedUpdate()
    {

        float velocityAdittion = 0;
        if (isSprinting)
            velocityAdittion = sprintAdittion;

        float directionX = inputHorizontal * (velocity + velocityAdittion) * Time.deltaTime;
        float directionZ = inputVertical * (velocity + velocityAdittion) * Time.deltaTime;
        float directionY = 0;
        
        if (isJumping)
        {
            directionY = Mathf.SmoothStep(jumpForce, jumpForce * 0.30f, jumpElapsedTime / jumpTime);
            directionY *= Time.deltaTime;

            jumpElapsedTime += Time.deltaTime;
            if (jumpElapsedTime >= jumpTime)
            {
                isJumping = false;
                jumpElapsedTime = 0;
            }
        }

        if( animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Slash") == false)
        {
            isAttacking = false;
            animator.SetBool("attack", false);
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
            //transform.rotation = rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }

        Vector3 verticalDirection = Vector3.up * directionY;
        Vector3 horizontalDirection = forward + right;

        Vector3 moviment = verticalDirection + horizontalDirection;
        cc.Move( moviment );
    }

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
