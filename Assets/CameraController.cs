using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public bool holdToMoveCamera = true;
    public float sensibility = 5f;
    public float cameraTopLimit = -45;
    public float cameraBottomLimit = -40f;

    Transform player;
    float offsetDistanceY;

    float mouseX;
    float mouseY;

    void Start()
    {

        if(holdToMoveCamera == false)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        player = GameObject.FindWithTag("Player").transform;
        offsetDistanceY = transform.position.y;
    }

    void Update()
    {

        transform.position = player.position + new Vector3(0, offsetDistanceY, 0);

        if(holdToMoveCamera == true )
            if (Input.GetAxisRaw("Fire2") == 0)
                return;

        mouseX += Input.GetAxis("Mouse X") * sensibility;
        mouseY += Input.GetAxis("Mouse Y") * sensibility;

        mouseY = Mathf.Clamp(mouseY, cameraTopLimit, cameraBottomLimit);

        transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);

    }
}


/* 
 
Configurar Câmera no joystick:
Edit > Project settings > input manager > axis
Duplicar Mouse X e duplicar mouse Y
Na cópia de Mouse X, alterar Type para Joystick Axis e Axis para 4th axis (Joystick)
Na cópia de Mouse Y, alterar Type para Joystick Axis e Axis para 5th axis (Joystick)
Marcar Mouse Y para invert pode ser interessante
 
 */