using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePortalWall : MonoBehaviour
{
    public GameObject wall;
	public Portal portal;

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "PortalCollider" || other.gameObject.layer == 9)
		{
			if(portal.OtherPortal != null) wall.GetComponent<Collider>().enabled = false;
		}
	}
}
