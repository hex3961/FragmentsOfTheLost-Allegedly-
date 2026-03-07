using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * 100f* mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * 100f* mouseSensitivity;
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
