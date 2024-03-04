using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerClickAgentController : MonoBehaviour
{

    Animator animator;
    NavMeshAgent agent;

    DestinyPoint destiny;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        destiny = GameObject.Find("Destiny").transform.GetComponent<DestinyPoint>();
    }

    // Update is called once per frame
    void Update()
    {

        animator.SetBool("run", agent.velocity.magnitude >= 0.2f);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
                destiny.Show(hit.point);
            }
        }

    }

}
