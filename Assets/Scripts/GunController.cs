using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
	// Variables
	public PlayerController Player;
	[Space]
	public GameObject GreenParticles;
	public GameObject RedParticles;
	[Space]
	public GameObject GreenPortalPrefab;
	public GameObject RedPortalPrefab;

	private void Portalable(int Portal, RaycastHit surface)
	{
		//Debug.Log("The surface of \"" + surface.transform.gameObject.name + "\" is Portalable.");
		GameObject newPortal;	
		float fY = 0;
		float fXZ = 0;
		float _SW = 0;
		float _SW2 = 0;

		if (Portal == 0)
		{
			Destroy(GameObject.Find(GreenPortalPrefab.name + "(Clone)"));
			newPortal = Instantiate(GreenPortalPrefab);
			Player.Portal1 = newPortal;
			newPortal.GetComponentInChildren<Portal>().Player = Player.transform;
			newPortal.GetComponentInChildren<Portal>().OtherPortal = GameObject.Find(RedPortalPrefab.name + "(Clone)");
			newPortal.GetComponentInChildren<DisablePortalWall>().wall = surface.transform.gameObject;

			MeshCollider meshCollider = surface.collider as MeshCollider;
			if (meshCollider == null || meshCollider.sharedMesh == null)
				return;

			Mesh mesh = meshCollider.sharedMesh;
			Vector3[] vertices = mesh.vertices;
			int[] triangles = mesh.triangles;
			Vector3 p0 = vertices[triangles[surface.triangleIndex * 3 + 0]];
			Vector3 p1 = vertices[triangles[surface.triangleIndex * 3 + 1]];
			Vector3 p2 = vertices[triangles[surface.triangleIndex * 3 + 2]];
			Transform hitTransform = surface.collider.transform;
			p0 = hitTransform.TransformPoint(p0);
			p1 = hitTransform.TransformPoint(p1);
			p2 = hitTransform.TransformPoint(p2);
			Debug.DrawLine(p0, p1);
			Debug.DrawLine(p1, p2);
			Debug.DrawLine(p2, p0);
			var v1 = p1 - p0;
			var v2 = p2 - p1;
			var normal = Vector3.Cross(v1, v2);
			newPortal.transform.forward = hitTransform.TransformDirection(normal);

			if (Mathf.Round(p1.y) == Mathf.Round(p2.y) && p0.y < p1.y) { fY = (p1 + p0).y / 2.0f; }
			if (Mathf.Round(p1.y) == Mathf.Round(p2.y) && p0.y > p1.y) { fY = (p1 + p0).y / 2.0f; }
			if (Mathf.Round(p0.y) == Mathf.Round(p2.y) && p1.y < p2.y) { fY = (p1 + p2).y / 2.0f; }
			if (Mathf.Round(p0.y) == Mathf.Round(p2.y) && p1.y > p2.y) { fY = (p1 + p2).y / 2.0f; }
			if (Mathf.Round(p0.y) == Mathf.Round(p1.y) && p2.y < p0.y) { fY = (p0 + p2).y / 2.0f; }
			if (Mathf.Round(p0.y) == Mathf.Round(p1.y) && p2.y > p0.y) { fY = (p0 + p2).y / 2.0f; }

			Debug.Log(Mathf.Round(newPortal.transform.forward.x));
			Debug.Log(Mathf.Round(newPortal.transform.forward.z));

			/*
				Z= -1 OR Z= 1:
					check X
				X= -1 OR X= 1:
					check Z
			*/

			if (Mathf.Round(newPortal.transform.forward.z) == -1 || Mathf.Round(newPortal.transform.forward.z) == 1)
			{
				_SW = 0;
				if (Mathf.Round(p1.x) == Mathf.Round(p2.x) && p1.x < p0.x)
				{
					// LEFT
					fXZ = p2.x + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p1.x) == Mathf.Round(p2.x) && p1.x > p0.x)
				{
					// RIGHT
					fXZ = p2.x - 0.595f;
					_SW2 = 0;
				}
				if (Mathf.Round(p0.x) == Mathf.Round(p2.x) && p0.x < p1.x)
				{
					// LEFT
					fXZ = p0.x + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p0.x) == Mathf.Round(p2.x) && p0.x > p1.x)
				{
					// RIGHT
					fXZ = p0.x - 0.595f;
					_SW2 = 0;
				}
				if (Mathf.Round(p0.x) == Mathf.Round(p1.x) && p0.x < p2.x)
				{
					// LEFT
					fXZ = p1.x + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p0.x) == Mathf.Round(p1.x) && p0.x > p2.x)
				{
					// RIGHT
					fXZ = p1.x - 0.595f;
					_SW2 = 0;
				}
			}
			else if (Mathf.Round(newPortal.transform.forward.x) == -1 || Mathf.Round(newPortal.transform.forward.x) == 1)
			{
				_SW = 1;
				if (Mathf.Round(p1.z) == Mathf.Round(p2.z) && p1.z < p0.z)
				{
					// LEFT
					fXZ = p2.z + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p1.z) == Mathf.Round(p2.z) && p1.z > p0.z)
				{
					// RIGHT
					fXZ = p2.z - 0.595f;
					_SW2 = 0;
				}
				if (Mathf.Round(p0.z) == Mathf.Round(p2.z) && p0.z < p1.z)
				{
					// LEFT
					fXZ = p0.z + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p0.z) == Mathf.Round(p2.z) && p0.z > p1.z)
				{
					// RIGHT
					fXZ = p0.z - 0.595f;
					_SW2 = 0;
				}
				if (Mathf.Round(p0.z) == Mathf.Round(p1.z) && p0.z < p2.z)
				{
					// LEFT
					fXZ = p1.z + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p0.z) == Mathf.Round(p1.z) && p0.z > p2.z)
				{
					// RIGHT
					fXZ = p1.z - 0.595f;
					_SW2 = 0;
				}
			}

			PortalReadyBig[] fixTriggers = newPortal.GetComponentsInChildren<PortalReadyBig>();
			foreach (var fixTrigger in fixTriggers)
			{
				fixTrigger.wall = surface.transform.gameObject;
			}
			if (Player.Portal2 != null)
			{
				Player.Portal2.GetComponentInChildren<Portal>().OtherPortal = newPortal;
			}
			newPortal.GetComponentInChildren<Camera>().targetTexture = Player.Portal2.GetComponentInChildren<Portal>().texture;
		}
		else
		{
			Destroy(GameObject.Find(RedPortalPrefab.name + "(Clone)"));
			newPortal = Instantiate(RedPortalPrefab);
			Player.Portal2 = newPortal;
			newPortal.GetComponentInChildren<Portal>().Player = Player.transform;
			newPortal.GetComponentInChildren<Portal>().OtherPortal = GameObject.Find(GreenPortalPrefab.name + "(Clone)");
			newPortal.GetComponentInChildren<DisablePortalWall>().wall = surface.transform.gameObject;

			MeshCollider meshCollider = surface.collider as MeshCollider;
			if (meshCollider == null || meshCollider.sharedMesh == null)
				return;

			Mesh mesh = meshCollider.sharedMesh;
			Vector3[] vertices = mesh.vertices;
			int[] triangles = mesh.triangles;
			Vector3 p0 = vertices[triangles[surface.triangleIndex * 3 + 0]];
			Vector3 p1 = vertices[triangles[surface.triangleIndex * 3 + 1]];
			Vector3 p2 = vertices[triangles[surface.triangleIndex * 3 + 2]];
			Transform hitTransform = surface.collider.transform;
			p0 = hitTransform.TransformPoint(p0);
			p1 = hitTransform.TransformPoint(p1);
			p2 = hitTransform.TransformPoint(p2);
			Debug.DrawLine(p0, p1);
			Debug.DrawLine(p1, p2);
			Debug.DrawLine(p2, p0);
			var v1 = p1 - p0;
			var v2 = p2 - p1;
			var normal = Vector3.Cross(v1, v2);
			newPortal.transform.forward = hitTransform.TransformDirection(normal);

			if (Mathf.Round(p1.y) == Mathf.Round(p2.y) && p0.y < p1.y) { fY = (p1 + p0).y / 2.0f; }
			if (Mathf.Round(p1.y) == Mathf.Round(p2.y) && p0.y > p1.y) { fY = (p1 + p0).y / 2.0f; }
			if (Mathf.Round(p0.y) == Mathf.Round(p2.y) && p1.y < p2.y) { fY = (p1 + p2).y / 2.0f; }
			if (Mathf.Round(p0.y) == Mathf.Round(p2.y) && p1.y > p2.y) { fY = (p1 + p2).y / 2.0f; }
			if (Mathf.Round(p0.y) == Mathf.Round(p1.y) && p2.y < p0.y) { fY = (p0 + p2).y / 2.0f; }
			if (Mathf.Round(p0.y) == Mathf.Round(p1.y) && p2.y > p0.y) { fY = (p0 + p2).y / 2.0f; }

			Debug.Log(Mathf.Round(newPortal.transform.forward.x));
			Debug.Log(Mathf.Round(newPortal.transform.forward.z));

			/*
				Z= -1 OR Z= 1:
					check X
				X= -1 OR X= 1:
					check Z
			*/

			if (Mathf.Round(newPortal.transform.forward.z) == -1 || Mathf.Round(newPortal.transform.forward.z) == 1)
			{
				_SW = 0;
				if (Mathf.Round(p1.x) == Mathf.Round(p2.x) && p1.x < p0.x)
				{
					// LEFT
					fXZ = p2.x + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p1.x) == Mathf.Round(p2.x) && p1.x > p0.x)
				{
					// RIGHT
					fXZ = p2.x - 0.595f;
					_SW2 = 0;
				}
				if (Mathf.Round(p0.x) == Mathf.Round(p2.x) && p0.x < p1.x)
				{
					// LEFT
					fXZ = p0.x + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p0.x) == Mathf.Round(p2.x) && p0.x > p1.x)
				{
					// RIGHT
					fXZ = p0.x - 0.595f;
					_SW2 = 0;
				}
				if (Mathf.Round(p0.x) == Mathf.Round(p1.x) && p0.x < p2.x)
				{
					// LEFT
					fXZ = p1.x + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p0.x) == Mathf.Round(p1.x) && p0.x > p2.x)
				{
					// RIGHT
					fXZ = p1.x - 0.595f;
					_SW2 = 0;
				}
			}
			else if (Mathf.Round(newPortal.transform.forward.x) == -1 || Mathf.Round(newPortal.transform.forward.x) == 1)
			{
				_SW = 1;
				if (Mathf.Round(p1.z) == Mathf.Round(p2.z) && p1.z < p0.z)
				{
					// LEFT
					fXZ = p2.z + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p1.z) == Mathf.Round(p2.z) && p1.z > p0.z)
				{
					// RIGHT
					fXZ = p2.z - 0.595f;
					_SW2 = 0;
				}
				if (Mathf.Round(p0.z) == Mathf.Round(p2.z) && p0.z < p1.z)
				{
					// LEFT
					fXZ = p0.z + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p0.z) == Mathf.Round(p2.z) && p0.z > p1.z)
				{
					// RIGHT
					fXZ = p0.z - 0.595f;
					_SW2 = 0;
				}
				if (Mathf.Round(p0.z) == Mathf.Round(p1.z) && p0.z < p2.z)
				{
					// LEFT
					fXZ = p1.z + 0.595f;
					_SW2 = 1;
				}
				if (Mathf.Round(p0.z) == Mathf.Round(p1.z) && p0.z > p2.z)
				{
					// RIGHT
					fXZ = p1.z - 0.595f;
					_SW2 = 0;
				}
			}

			bool zNeg = surface.transform.rotation.z < 0;
			bool xNeg = surface.transform.rotation.x < 0;
			bool zPos = surface.transform.rotation.z > 0;
			bool xPos = surface.transform.rotation.x > 0;

			if (xPos || xNeg)
			{
				newPortal.transform.rotation =
					Quaternion.Euler(newPortal.transform.rotation.x - surface.transform.rotation.x, newPortal.transform.rotation.y, newPortal.transform.rotation.z);
			} else if (zPos || zNeg)
			{
				newPortal.transform.rotation =
					Quaternion.Euler(newPortal.transform.rotation.x, newPortal.transform.rotation.y, newPortal.transform.rotation.z - surface.transform.rotation.z);
			}
			else if ((zPos && xPos) || (zNeg && xNeg) || (zPos && xNeg) || (zNeg && xPos))
			{
				newPortal.transform.rotation =
					Quaternion.Euler(newPortal.transform.rotation.x - surface.transform.rotation.x, newPortal.transform.rotation.y, newPortal.transform.rotation.z - surface.transform.rotation.z);
			}

			PortalReadyBig[] fixTriggers = newPortal.GetComponentsInChildren<PortalReadyBig>();
			foreach (var fixTrigger in fixTriggers)
			{
				fixTrigger.wall = surface.transform.gameObject;
			}
			if (Player.Portal1 != null)
			{
				Player.Portal1.GetComponentInChildren<Portal>().OtherPortal = newPortal;
			}
			newPortal.GetComponentInChildren<Camera>().targetTexture = Player.Portal1.GetComponentInChildren<Portal>().texture;
		}

		newPortal.GetComponentInChildren<ParticleSystem>().Play();
		newPortal.transform.position = surface.point;
		newPortal.transform.Translate(0, 0, 0.01f);
		if (_SW == 0)
		{
			if (_SW2 == 0)
			{
				if (fXZ < newPortal.transform.position.x)
				{
					newPortal.transform.position = new Vector3(fXZ, fY, newPortal.transform.position.z);
				}
				else
				{
					newPortal.transform.position = new Vector3(newPortal.transform.position.x, fY, newPortal.transform.position.z);
				}
			} else
			{
				if (fXZ > newPortal.transform.position.x)
				{
					newPortal.transform.position = new Vector3(fXZ, fY, newPortal.transform.position.z);
				}
				else
				{
					newPortal.transform.position = new Vector3(newPortal.transform.position.x, fY, newPortal.transform.position.z);
				}
			}
			
		} else
		{
			if (_SW2 == 0)
			{
				if (fXZ < newPortal.transform.position.z)
				{
					newPortal.transform.position = new Vector3(newPortal.transform.position.x, fY, fXZ);
				}
				else
				{
					newPortal.transform.position = new Vector3(newPortal.transform.position.x, fY, newPortal.transform.position.z);
				}
			}
			else
			{
				if (fXZ > newPortal.transform.position.z)
				{
					newPortal.transform.position = new Vector3(newPortal.transform.position.x, fY, fXZ);
				}
				else
				{
					newPortal.transform.position = new Vector3(newPortal.transform.position.x, fY, newPortal.transform.position.z);
				}
			}
		}

		if (surface.transform.gameObject.name.Contains("[SLANTED]"))
		{
			Debug.Log("SLANTED, IGNORE EVERY OTHER LINE OF BULLSHIT AND FIX THIS MFS ROTATION & POSITION.");
			Debug.Log(surface.transform.position);
			newPortal.transform.position = new Vector3(surface.transform.position.x, surface.transform.position.y + 0.005f, surface.transform.position.z);
			newPortal.transform.rotation = Quaternion.Euler(-(90-surface.transform.rotation.eulerAngles.x), surface.transform.rotation.eulerAngles.y, surface.transform.rotation.eulerAngles.z);
			Debug.Log(surface.transform.forward);
		}
	}

	private void notPortalable(RaycastHit surface)
	{
		Debug.Log("The surface of \"" + surface.transform.gameObject.name + "\" is not Portalable.");
	}

	public void Shoot(int port, Camera cam)
	{
		Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
		RaycastHit hit;

		if(Physics.Raycast(rayOrigin, cam.transform.forward, out hit, 9000f))
		{
			if (hit.collider.gameObject.layer == 8) Portalable(port, hit);
			else notPortalable(hit);
		}
	}
}
