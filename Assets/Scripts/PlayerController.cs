using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Base Values")]
    public Text verText;
    public bool verShow;
    public CharacterController controller;
    public GameObject DragCamera;
    public Camera Cam;
    public bool hasGun = true;
    public bool isNoClip = false;
    public LayerMask pLayer;
    [Space]
	[Header("Movement Values")]
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jump = 1f;
    [Space]
    [Header("Ground Checking")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    [Space]
    [Header("Portals and Portal Gun")]
    public GameObject Portal1;
    public GameObject Portal2;
    public GameObject PortalGun;
    public bool GreenGunOnly = false;
    [Space]

    private bool waitingToTP = false;
    private Vector3 tpLoc;
    private Quaternion tpRot;

    Vector3 velocity;
    bool isGrounded;

    private void Awake()
    {
        if (hasGun) PortalGun.GetComponent<Animation>().Play("opening_0");
        //if (verShow) verText.text = Application.version;
    }

		void Update()
    {
        if (!isNoClip) isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            if (!isNoClip) velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        
        if (isNoClip)
        {
            move = Cam.transform.forward * 0;
            float speeds = 1;
            if (Input.GetKey(KeyCode.LeftShift)) speeds = 3;
            if (!Input.GetKey(KeyCode.LeftShift)) speeds = 1;
            if (Input.GetKey(KeyCode.W)) move = Cam.transform.forward * speeds;
            if (Input.GetKey(KeyCode.S)) move = Cam.transform.forward * -speeds;
            if (Input.GetKey(KeyCode.A)) move = Cam.transform.right * -speeds;
            if (Input.GetKey(KeyCode.D)) move = Cam.transform.right * speeds;
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) move = Cam.transform.forward * speeds + Cam.transform.right * speeds;
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) move = Cam.transform.forward * speeds + Cam.transform.right * -speeds;
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) move = Cam.transform.forward * -speeds + Cam.transform.right * speeds;
            if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)) move = Cam.transform.forward * -speeds + Cam.transform.right * -speeds;
        
        }

        controller.Move(move * speed * Time.deltaTime);
        //Cam.transform.Rotate(new Vector3(0f, 0f, Input.GetAxis("Horizontal") * 1.5f));

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        if (!isNoClip) velocity.y += gravity * Time.deltaTime;

        if (!isNoClip) controller.Move(velocity * Time.deltaTime);

        /* -- GUN -- */

		if (Input.GetMouseButtonDown(0))
		{
            if (hasGun) PortalGun.GetComponent<Animation>().Play("firegreen");
            if (hasGun) PortalGun.GetComponent<GunController>().Shoot(0, Cam);
		} else if (Input.GetMouseButtonDown(1))
        {
            if(!GreenGunOnly)
			{
                if (hasGun) PortalGun.GetComponent<Animation>().Play("firered");
                if (hasGun) PortalGun.GetComponent<GunController>().Shoot(1, Cam);
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            if (!isNoClip)
            {
                isNoClip = true;
                Collider col = GetComponent<Collider>();
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.useGravity = false;
                controller.detectCollisions = false;
                rb.detectCollisions = false;
                col.enabled = false;

                for (var i = 0; i <= 31; i++)
                {
                    Physics.IgnoreLayerCollision(6, i, true);
                }
            }
            else
            {
                isNoClip = false;
                Collider col = GetComponent<Collider>();
                Rigidbody rb = GetComponent<Rigidbody>();
                rb.useGravity = true;
                controller.detectCollisions = true;
                rb.detectCollisions = true;
                col.enabled = true;

                for(var i=0;i<=31;i++)
				{
                    Physics.IgnoreLayerCollision(6, i, false);
                }
            }
        }
    }

	private void LateUpdate()
	{
        
        if (waitingToTP)
        {
            transform.position = tpLoc;
            transform.rotation = tpRot;
            waitingToTP = false;
        }
    }

    public void waitForTP(Vector3 tpLe, Quaternion tpLr)
	{
        tpLoc = tpLe;
        tpRot = tpLr;
        waitingToTP = true;
    }

    public void ResetZone()
	{
        if (Portal1 != null) Destroy(Portal1);
        if (Portal2 != null) Destroy(Portal2);

        Portal1 = null;
        Portal2 = null;
    }
}
