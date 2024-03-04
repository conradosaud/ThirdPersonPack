using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    Transform player;
    float offsetDistanceY;

    float mouseX;
    float mouseY;

    public float sensibility = 5f;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        offsetDistanceY = transform.position.y;
    }

    void Update()
    {

        transform.position = player.position + new Vector3(0, offsetDistanceY, 0);

        if (Input.GetKey(KeyCode.Mouse1) == false)
            return;

        mouseX += Input.GetAxis("Mouse X") * sensibility;
        mouseY += Input.GetAxis("Mouse Y") * sensibility;

        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);

    }
}
