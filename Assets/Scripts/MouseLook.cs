using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    public float DragSpeed = 0f;
    [Space]
    public GameObject DragCamera;
    public GameObject HeldObjectPositioner;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * MouseX);

        DragCamera.transform.rotation = transform.rotation;
    }

	private void LateUpdate()
	{

        DragCamera.transform.position = new Vector3(playerBody.position.x, playerBody.position.y + 0.27751274f, playerBody.position.z); ;
        HeldObjectPositioner.transform.rotation = transform.rotation;
    }
}