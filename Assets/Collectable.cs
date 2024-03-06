using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if( Input.GetKey(KeyCode.E))
        {
            if (other.gameObject.tag == "Player")
            {
                Transform player = GameObject.FindWithTag("Player").transform;
            }
        }
    }

}
