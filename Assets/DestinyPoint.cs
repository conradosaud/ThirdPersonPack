using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinyPoint : MonoBehaviour
{

    public float rotateSpeed = 1f;
    Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void FixedUpdate()
    {
        transform.Rotate( 0, 0, rotateSpeed * Time.deltaTime  );
    }

    public void Show( Vector3 point)
    {
        transform.position = point + new Vector3(0, 0.2f, 0) ;
        renderer.enabled = true;
    }
    public void Hide()
    {
        renderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            renderer.enabled = false;
    }
}
