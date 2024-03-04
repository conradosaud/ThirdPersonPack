using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerClickPersistentController : MonoBehaviour
{

    public float gravity = 9.8f;
    public float velocity = 5f;

    int groundLayer;

    Animator animator;
    CharacterController cc;

    DestinyPoint destiny;

    // Start is called before the first frame update
    void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
        animator = GetComponent<Animator>();
        destiny = GameObject.Find("Destiny").transform.GetComponent<DestinyPoint>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetBool("run", false);

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, groundLayer))
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = transform.position.y; // Maintain the same height

                // Calculate direction towards the target
                moveDirection = (targetPosition - transform.position).normalized;

                // Move towards the target


                destiny.Show(hit.point);

                float originalX = transform.rotation.eulerAngles.x;
                transform.LookAt(hit.point);
                transform.rotation = Quaternion.Euler(originalX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                animator.SetBool("run", true);
            }
        }
        else
        {
            destiny.Hide();
        }

        moveDirection.y = - gravity * Time.deltaTime;
        cc.Move(moveDirection * velocity * Time.deltaTime);

        

    }

}
