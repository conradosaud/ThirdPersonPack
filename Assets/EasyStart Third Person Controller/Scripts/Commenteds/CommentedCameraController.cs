
using UnityEngine;

/// <summary>
/// Camera movement script for third person games.
/// This Script should not be applied to the camera! It is attached to an empty object and inside
/// it (as a child object) should be your game's MainCamera.
/// </summary>
public class CommentedCameraController : MonoBehaviour
{

    [Tooltip("Enable to move the camera by holding the right mouse button. Does not work with joysticks (keep false).")]
    public bool clickToMoveCamera = false;
    [Tooltip("Enable zoom in/out when scrolling the mouse wheel. Does not work with joysticks.")]
    public bool canZoom = true;
    [Space]
    [Tooltip("The higher it is, the faster the camera moves. It is recommended to increase this value for games that uses joystick.")]
    public float sensitivity = 5f;

    // Defines the maximum that the camera can rotate on the Y axis. Explanation below.
    [Tooltip("Camera Y rotation limits. The X axis is the maximum it can go up and the Y axis is the maximum it can go down.")]
    public Vector2 cameraLimit = new Vector2(-45, 40);

    // Mouse coordinates on the screen
    float mouseX;
    float mouseY;

    // Minimum camera offset, so that it does not enter the player's body
    float offsetDistanceY;

    // Get the player position, rotation, scale, etc
    Transform player;


    void Start()
    {

        // If the option to move the camera with click is disabled,
        // then it is more comfortable to hide the mouse from the screen, as the player will not use it
        if (clickToMoveCamera == false)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked; // Makes the mouse stick to the center of the screen
            UnityEngine.Cursor.visible = false; // Disappears with the cursor
        }

        // Gets the player object on screen when starting the game
        player = GameObject.FindWithTag("Player").transform;

        // Sets the minimum distance between the camera and the character.
        offsetDistanceY = transform.position.y;
        // You can adjust how far the camera is from the character through the CameraController prefab
        // Just move the Y position away and this variable will automatically save this value for every time your game starts
    }


    void Update()
    {
        // Makes the camera position the same as the player's with a defined offset setback
        // This way, the camera will follow you, but maintaining the minimum offset
        transform.position = player.position + new Vector3(0, offsetDistanceY, 0);

        // If the camera zoom is enabled, set camera zoom when mouse wheel is scrolled
        if (canZoom && Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            // To zoom in or out, we can change the camera's transform, but it's easier to just set the field of view
            Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * sensitivity * 2;
        }

        // If the option to move camera with button is enabled
        if (clickToMoveCamera == true)
        {
            // If it is enabled, check that it IS NOT being pressed now
            if (Input.GetAxisRaw("Fire2") == 0)
            {
                // If it isn't, this command causes nothing to run from here down, and the Update ends
                return;
            }
        }

        // Get and calculates the current mouse position on the screen
        mouseX += Input.GetAxis("Mouse X") * sensitivity;
        mouseY += Input.GetAxis("Mouse Y") * sensitivity;

        //This function limits the maximum value that the camera can rotate.
        // If you remove this, you'll notice that the camera can rotate 360 ​​degrees around the character
        // https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html
        mouseY = Mathf.Clamp(mouseY, cameraLimit.x, cameraLimit.y);

        // We make the calculated position of the mouse rotate the camera
        transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0);

    }
}

/* 
 
------------- ATTENTION ------------- 
Normally, we use the right analog stick of the joystick to move the camera. It is not configured by default in Unity.
If you don't intend to adapt your controller to joysticks, just ignore this.

If you want to make the right analog stick work, follow the steps:
1. In unity, follow this path in the menu: Edit > Project Settings > Input Manager > Axis
2. Duplicate Mouse X and Nouse Y
3. On Mouse X copy, change Type to Joystick Axis and Axis to 4th Axis (Joystick)
4. On Mouse Y copy, change Type to Joystick Axis and Axis to 5th Axis (Joystick)
5. Check Mouse Y to "Invert" sounds good

Check out the PDF tutorial file in this pack for more detailed information with images
 */