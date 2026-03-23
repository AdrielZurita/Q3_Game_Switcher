using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameramovement : MonoBehaviour
{
        
    public float sensitivityX;
    public float sensitivityY;

    public Transform orientation;
    public Transform cameraPosition;


    private float xRotation;
    private float yRotation;

    public ObjectPlsHelp objectPlsHelp;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        objectPlsHelp.canCam = true;
    }

    // Update is called once per frame
    void Update()
    {
        sensitivityX = objectPlsHelp.sens;
        sensitivityY = objectPlsHelp.sens;
        if (objectPlsHelp.canCam == true)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivityX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivityY;

            yRotation += mouseX;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, yRotation, transform.localRotation.eulerAngles.z);
            orientation.localRotation = Quaternion.Euler(orientation.localRotation.eulerAngles.x, yRotation, 0);
            cameraPosition.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        }
    }
}
