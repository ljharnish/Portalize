using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	public GameObject OtherPortal;
	public DisablePortalWall wallDisabler;
	public MeshRenderer PORTAL;
	public Transform teleportPoint;
	public Transform teleportPointItems;
	public Transform Player;
	public GameObject Camera;

	public RenderTexture texture;
	public Material material;
	public Shader shader;

	private void Start()
	{
		//material = new Material(shader);
		texture = new RenderTexture(Screen.width, Screen.height, 1);
		material.SetTexture("_MainTex", texture);
		PORTAL.material = material;
		OtherPortal.transform.GetChild(2).GetComponent<Portal>().Camera.GetComponent<Camera>().targetTexture = texture;

		Player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.name == "PortalCollider")
		{
			wallDisabler.wall.GetComponent<Collider>().enabled = false;
			Vector3 tp = OtherPortal.GetComponentInChildren<Portal>().teleportPoint.transform.position;

			/*Quaternion tpr = new Quaternion(
				 (transform.rotation.x%360)-(OtherPortal.GetComponentInChildren<Portal>().transform.rotation.x%360)+(Player.rotation.x%360),
				 (transform.rotation.y%360)-(OtherPortal.GetComponentInChildren<Portal>().transform.rotation.y%360)+(Player.rotation.y%360),
				 (transform.rotation.z%360)-(OtherPortal.GetComponentInChildren<Portal>().transform.rotation.z%360)+(Player.rotation.z%360),
				 (transform.rotation.w%360)-(OtherPortal.GetComponentInChildren<Portal>().transform.rotation.w%360)+(Player.rotation.w%360)
			);*/

			Quaternion tpr = OtherPortal.GetComponentInChildren<Portal>().transform.rotation;

			OtherPortal.transform.GetChild(2).gameObject.SetActive(false);
			other.transform.parent.GetComponent<PlayerController>().waitForTP(tp, tpr);
		}

		if (other.gameObject.layer == 9)
		{
			wallDisabler.wall.GetComponent<Collider>().enabled = false;
			Vector3 tp = OtherPortal.GetComponentInChildren<Portal>().teleportPointItems.transform.position;
			Quaternion tpr = OtherPortal.GetComponentInChildren<Portal>().transform.rotation;

			//OtherPortal.transform.GetChild(2).gameObject.SetActive(false);
			other.gameObject.transform.position = tp;
			other.gameObject.transform.rotation = tpr;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.name == "PortalCollider" || other.gameObject.layer == 9)
		{
			wallDisabler.wall.GetComponent<Collider>().enabled = true;
		}
	}

	private void Update()
	{
		Transform playerCam = Player.GetComponentInChildren<Camera>().gameObject.transform;
		Transform camTrans = Camera.transform;
		Transform partnerTrans = OtherPortal.transform;
		Vector3 cameraEuler = Vector3.zero;
		Vector3 pos = partnerTrans.InverseTransformPoint(playerCam.position);
		camTrans.localPosition = new Vector3(-pos.x, pos.y, -pos.z);
		Transform prevParent = playerCam.parent;
		playerCam.SetParent(transform);
		cameraEuler.x = playerCam.localEulerAngles.x;
		playerCam.SetParent(prevParent);
		Vector3 oldPlayerRot = playerCam.localEulerAngles;
		playerCam.localRotation = Quaternion.Euler(0, oldPlayerRot.y, oldPlayerRot.z);
		cameraEuler.y = SignedAngle(-partnerTrans.forward, playerCam.forward, Vector3.up);
		playerCam.localRotation = Quaternion.Euler(oldPlayerRot);
		camTrans.localRotation = Quaternion.Euler(cameraEuler);
	}

	float SignedAngle(Vector3 a, Vector3 b, Vector3 n)
	{
		float angle = Vector3.Angle(a, b);
		float sign = Mathf.Sign(Vector3.Dot(n, Vector3.Cross(a, b)));
		float signed_angle = angle * sign;
		while (signed_angle < 0) signed_angle += 360;
		return signed_angle;
	}
}
