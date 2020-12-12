using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerBody;
    public Transform cameraHolder;

    public float sens;

    public float currentY;

    public void Start()
    {
        currentY = 0;
    }

    public void Update()
    {
        float inputX = Input.GetAxisRaw("Mouse X");
        float inputY = Input.GetAxisRaw("Mouse Y");

        currentY -= inputY;
        currentY = Mathf.Clamp(currentY, -90, 90);

        if (currentY < -90)
        {
            currentY = -89.9f;
        }
        else if (currentY > 90)
        {
            currentY = 89.9f;
        }

        playerBody.Rotate(new Vector3(0, inputX * Time.deltaTime * sens));
        cameraHolder.localRotation = Quaternion.Euler(new Vector3(currentY, 0, 0));
    }
}
