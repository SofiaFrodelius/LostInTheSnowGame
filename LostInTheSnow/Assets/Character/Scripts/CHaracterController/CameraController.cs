using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 1.0f;
    private Vector2 look = Vector2.zero;
    private Transform playerTransform;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerTransform = GameObject.FindWithTag("Player").transform;
        look = new Vector2(playerTransform.eulerAngles.y, transform.eulerAngles.x);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector2 controllerMove = new Vector2(Input.GetAxisRaw("Right Stick Horizontal"), Input.GetAxisRaw("Right Stick Vertical"));
        move *= sensitivity;
        controllerMove *= sensitivity;
        look += move;
        look += controllerMove;
        look.y = Mathf.Clamp(look.y, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(-look.y, Vector3.right);
        playerTransform.localRotation = Quaternion.AngleAxis(look.x, playerTransform.up);

        

        //Temp
        if (Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            Cursor.lockState = CursorLockMode.Locked;

    }

    public Vector2 getLook()
    {
        return look;
    }

    public void setLook(Vector2 l)
    {
        look = l;
    }

}
